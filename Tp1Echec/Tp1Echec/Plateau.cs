<<<<<<< HEAD
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Tp1Echec
{
    public class Plateau
    {

        // Attributs

        // Grille 0-based : _grillage[col, row] avec col 0=a..7=h, row 0=rangée 1..7=rangée 8.
        // Les blancs démarrent rows 0-1, les noirs rows 6-7.
        private Piece[,] _grillage;

        // Mémorise la position du pion qui vient de faire un double avance (pour la prise en passant).
        // Null si le dernier coup n'était pas un double avance de pion.
        // Réinitialisé à null au début de chaque JouerCoup.
        private (int, int)? _dernierPionDoubleAvance;

        // Constructeur

        public Plateau()
        {
            _grillage = new Piece[8, 8];
            _dernierPionDoubleAvance = null;
        }

        public Plateau(Plateau plateau)
        {
            _grillage = new Piece[8, 8];
            _dernierPionDoubleAvance = plateau._dernierPionDoubleAvance;
            for (int i = 0; i < 8; i++)
            {
                for(int j = 0; j < 8; j++)
                {
                    if (plateau._grillage[i, j] != null)
                        _grillage[i, j] = plateau._grillage[i, j].Copier();
                }
            }
        }

        // Indexeur

        private Piece this[int x, int y]
        {
            get { return _grillage[x, y]; }
            set { _grillage[x, y] = value; }
        }

        // Méthodes

        // DONE
        // Initialise le plateau en position de départ standard des échecs.
        // Utilise des indices 0-based : col 0=a..7=h, row 0=rangée 1..7=rangée 8.
        // Les blancs occupent rows 0-1, les noirs rows 6-7.
        // Toutes les pièces reçoivent pieceNaPasBouge=true pour activer les coups spéciaux initiaux (roque, double avance).
        public void InitialiserPlateau()
        {
            // Vider toutes les cases
            for (int col = 0; col < 8; col++)
                for (int row = 0; row < 8; row++)
                    _grillage[col, row] = null;

            // Row 0 : pièces majeures blanches (ordre standard : Tour, Cavalier, Fou, Dame, Roi, Fou, Cavalier, Tour)
            _grillage[0, 0] = new Tour(true, true);
            _grillage[1, 0] = new Cavalier(true, true);
            _grillage[2, 0] = new Fou(true, true);
            _grillage[3, 0] = new Dame(true, true);
            _grillage[4, 0] = new Roi(true, true);
            _grillage[5, 0] = new Fou(true, true);
            _grillage[6, 0] = new Cavalier(true, true);
            _grillage[7, 0] = new Tour(true, true);

            // Row 1 : pions blancs
            for (int col = 0; col < 8; col++)
                _grillage[col, 1] = new Pion(true, true);

            // Rows 2-5 : vides (déjà null après le vidage)

            // Row 6 : pions noirs
            for (int col = 0; col < 8; col++)
                _grillage[col, 6] = new Pion(false, true);

            // Row 7 : pièces majeures noires (même ordre que row 0)
            _grillage[0, 7] = new Tour(false, true);
            _grillage[1, 7] = new Cavalier(false, true);
            _grillage[2, 7] = new Fou(false, true);
            _grillage[3, 7] = new Dame(false, true);
            _grillage[4, 7] = new Roi(false, true);
            _grillage[5, 7] = new Fou(false, true);
            _grillage[6, 7] = new Cavalier(false, true);
            _grillage[7, 7] = new Tour(false, true);

            _dernierPionDoubleAvance = null;
        }

        // DONE
        // Vérifie si des coordonnées 0-based sont dans les limites du plateau 8x8.
        // Doit être appelée avant tout accès à _grillage pour éviter les exceptions d'index hors bornes.
        // Simple vérification d'intervalle : 0 <= x < 8 et 0 <= y < 8.
        public bool PositionDansPlateau(int x, int y)
        {
            return x >= 0 && x < 8 && y >= 0 && y < 8;
        }

        // DONE
        // Sérialise l'état du plateau en texte lisible pour le débogage ou l'export.
        // Chaque case est représentée par Serilization() de la pièce ou "Vide", séparées par "|".
        // Une nouvelle ligne sépare chaque rangée (row 0 à row 7).
        // 32 pièces attendues en position initiale, 32 cases "_".
        public string serilizationPlateau()
        {
            StringBuilder sb = new StringBuilder();
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    if (col > 0) sb.Append(",");
                    Piece p = _grillage[col, row];
                    sb.Append(p != null ? p.Serilization() : "_");
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }

        // FONCTION AvoirPiecesEnnemi
        // Parse le plateau et retourne une List<Piece> de toutes les pieces ennemies
        // Recoit en paramètre une couleur (avec le booléen).
        public List<Piece> AvoirPiecesEnnemi(bool estBlanc)
        {
            List<Piece> piecesEnnemies = new List<Piece>();
            for (int col = 0; col < 8; col++)
                for (int row = 0; row < 8; row++)
                {
                    Piece p = _grillage[col, row];
                    if (p != null && p.PieceEstBlanche != estBlanc)
                        piecesEnnemies.Add(p);
                }
            return piecesEnnemies;
        }

        // Valide un coup complet en appliquant toutes les règles du jeu dans l'ordre.
        // Retourne false dès qu'une règle est violée (validation en court-circuit).
        // Délègue la géométrie à piece.ValiderCoup, le contexte du pion à ValiderCoupPion,
        // le roque à ValiderRoque, et vérifie via SimulerCoup qu'on ne se met pas en échec.
        public bool ValiderCoup(Coup coup)
        {
            int x1 = coup.posDebut.Item1, y1 = coup.posDebut.Item2;
            int x2 = coup.posFin.Item1, y2 = coup.posFin.Item2;

            // 1. Positions dans les bornes du plateau
            if (!(PositionDansPlateau(x1, y1) && PositionDansPlateau(x2, y2)))
                return false;

            // 2. Une pièce doit être présente à la position de départ
            Piece piece = _grillage[x1, y1];
            if (piece == null)
                return false;

            // 3. La pièce doit appartenir au joueur dont c'est le tour
            if (piece.PieceEstBlanche != coup.estTourBlanc)
                return false;

            // 4. La destination ne doit pas être occupée par une pièce amie (sauf roque : la Tour est alliée)
            Piece destination = _grillage[x2, y2];
            if (destination != null && destination.PieceEstBlanche == coup.estTourBlanc
                && !(piece.PeutInitierRoque() && !piece.PieceABouge() && destination.PeutSuivreRoque() && !destination.PieceABouge()))
                return false;

            int dx = x2 - x1;
            int dy = y2 - y1;

            // 4.5 Roque : si le Roi capture la Tour alliée, déléguer entièrement à ValiderRoque
            if (piece.PeutInitierRoque() && !piece.PieceABouge()
                && destination != null && destination.PeutSuivreRoque() && !destination.PieceABouge())
                return ValiderRoque(coup);

            // 5. Validation géométrique déléguée à la pièce
            if (!piece.ValiderCoup(coup))
                return false;

            // 6. Règles contextuelles du pion
            if (!ValiderCoupsPion(coup, piece, destination, dx, dy))
                return false;

            // 7. Chemin libre pour les pièces qui ne peuvent pas sauter par-dessus d'autres pièces
            if (piece.CauseCollision())
            {
                if (!CheminLibre(coup))
                    return false;
            }

            // 8. Rejeter tout coup qui laisserait son propre roi en échec
            if (SimulerCoup(coup).VerificationEchec(coup.estTourBlanc))
                return false;

            return true;
        }

        // Valide les règles contextuelles propres au pion.
        // Retourne true si aucune règle du pion n'est violée (ou si la pièce n'est pas un pion).
        private bool ValiderCoupsPion(Coup coup, Piece piece, Piece destination, int dx, int dy)
        {
            int x1 = coup.posDebut.Item1, y1 = coup.posDebut.Item2;
            int x2 = coup.posFin.Item1;

            // Direction : blanc avance vers +y, noir vers -y
            if (piece.CollisionParDevant())
            {
                int directionAttendue = coup.estTourBlanc ? 1 : -1;
                if (Math.Sign(dy) != directionAttendue)
                    return false;
            }

            // Bloquer l'avance droite si la case est occupée
            if (dx == 0 && (Math.Abs(dy) == 1 || Math.Abs(dy) == 2) && piece.CollisionParDevant() && destination != null)
                return false;

            // Prise diagonale : destination doit contenir un ennemi, sauf en passant
            if (Math.Abs(dx) == 1 && Math.Abs(dy) == 1 && piece.PriseParDiagonal() && destination == null)
            {
                if (!_dernierPionDoubleAvance.HasValue)
                    return false;
                (int epCol, int epRow) = _dernierPionDoubleAvance.Value;
                if (epCol != x2 || epRow != y1)
                    return false;
            }

            // Charge (double avance) : pion sur sa rangée initiale, case intermédiaire et destination vides
            if (piece.PeutCharger() && Math.Abs(dy) == 2 && dx == 0)
            {
                if (piece.PieceABouge())
                    return false;
                int rowInitial = coup.estTourBlanc ? 1 : 6;
                if (y1 != rowInitial)
                    return false;
                int yMilieu = y1 + Math.Sign(dy);
                if (_grillage[x1, yMilieu] != null)
                    return false;
            }

            return true;
        }

        // DONE
        // Vérifie que toutes les cases entre la position de départ et d'arrivée sont vides.
        // Utilisé pour les pièces à déplacement glissant (Tour, Fou, Dame) qui ne peuvent pas sauter.
        // Le signe de dx/dy détermine la direction du parcours case par case.
        // Les cases de départ et d'arrivée sont exclues (l'arrivée peut être une capture légale).
        private bool CheminLibre(Coup coup)
        {
            int x1 = coup.posDebut.Item1, y1 = coup.posDebut.Item2;
            int x2 = coup.posFin.Item1,  y2 = coup.posFin.Item2;

            int stepX = Math.Sign(x2 - x1);
            int stepY = Math.Sign(y2 - y1);

            int x = x1 + stepX;
            int y = y1 + stepY;

            while (x != x2 || y != y2)
            {
                if (_grillage[x, y] != null)
                    return false;
                x += stepX;
                y += stepY;
            }
            return true;
        }

        // Les méthodes PieceABouge(): bool, PeutInitierRoque(): bool et PeutSuivreRoque(): bool vont te permettre de vérifier si le roque peut être fait.
        // 1. Valide si le roque peut être fait. (SINON early return)
        // 2. Si correct, faire la validation que le chemin pour le roi est Safe (copier plateau, null où le roi est, nouveau roi à la première case intermédiaire, vérifierEchec(), si good, on recommence, jusqu'à atteindre la case destination) early return sinon.
        // 3. Si tout est parfait, on retourne true.
        private bool ValiderRoque(Coup coup)
        {
            int x1 = coup.posDebut.Item1, y1 = coup.posDebut.Item2;
            int x2 = coup.posFin.Item1,  y2 = coup.posFin.Item2;

            // Le roque est horizontal (même rangée)
            if (y1 != y2) return false;

            Piece roi = _grillage[x1, y1];
            Piece tour = _grillage[x2, y2];

            // 1. Vérifications polymorphiques : Roi et Tour n'ont pas bougé
            if (roi == null || !roi.PeutInitierRoque() || roi.PieceABouge()) return false;
            if (tour == null || !tour.PeutSuivreRoque() || tour.PieceABouge()) return false;

            // Chemin libre entre le roi et la tour (cases intermédiaires)
            if (!CheminLibre(coup)) return false;

            // 2. Le roi ne doit pas être en échec sur sa case actuelle, de transit, ni d'arrivée
            // On simule le roi à chaque case traversée (case 0 = départ, 1 = transit, 2 = arrivée finale)
            int direction = Math.Sign(x2 - x1);
            for (int pas = 0; pas <= 2; pas++)
            {
                int caseRoi = x1 + pas * direction;
                Plateau simulation = new Plateau(this);
                simulation._grillage[x1, y1] = null;
                simulation._grillage[caseRoi, y1] = roi;
                if (simulation.VerificationEchec(coup.estTourBlanc))
                    return false;
            }

            // 3. Tout est valide
            return true;
        }

        // Very bad (pas contente).
        // Exécute un coup validé en appliquant tous les effets de bord sur le plateau.
        // Cas spéciaux gérés dans l'ordre : prise en passant, roque (déplacement Tour), déplacement principal,
        // mémorisation du double avance, promotion automatique en Dame à la dernière rangée.
        // SetPieceABouge(true) désactive les coups spéciaux futurs (roque, double avance du pion).
        public void JouerCoup(Coup coup)
        {
            int x1 = coup.posDebut.Item1, y1 = coup.posDebut.Item2;
            int x2 = coup.posFin.Item1,  y2 = coup.posFin.Item2;

            int dx = x2 - x1;
            int dy = y2 - y1;

            Piece piece = _grillage[x1, y1];

            // Réinitialiser la prise en passant : elle n'est valide que pendant le coup immédiatement suivant
            _dernierPionDoubleAvance = null;

            // Prise en passant : le pion se déplace en diagonale vers une case vide
            // Le pion ennemi capturé se trouve sur (x2, y1) — même colonne que la destination, même rangée que le pion
            if (piece.PeutPrendreEnPassant() && Math.Abs(dx) == 1 && _grillage[x2, y2] == null)
                _grillage[x2, y1] = null;

            // Roque : le Roi encode le coup vers la case de la Tour alliée
            // On place le Roi à 2 cases dans la direction de la Tour, et la Tour juste à côté du Roi
            if (piece.PeutInitierRoque() && !piece.PieceABouge()
                && _grillage[x2, y2] != null && _grillage[x2, y2].PeutSuivreRoque() && !_grillage[x2, y2].PieceABouge())
            {
                Piece tour = _grillage[x2, y2];
                int direction = Math.Sign(dx);

                // Effacer les positions originales du Roi et de la Tour
                _grillage[x1, y1] = null;
                _grillage[x2, y2] = null;

                // Petit roque (direction=+1) : Roi → col 6, Tour → col 5
                // Grand roque (direction=-1) : Roi → col 2, Tour → col 3
                _grillage[x1 + 2 * direction, y1] = piece;
                _grillage[x1 + 1 * direction, y1] = tour;

                piece.SetPieceABouge(true);
                tour.SetPieceABouge(true);
                return;
            }

            // Déplacement normal : déplacer la pièce à sa destination
            _grillage[x2, y2] = piece;
            _grillage[x1, y1] = null;
            piece.SetPieceABouge(true);

            // Mémoriser le double avance du pion pour autoriser la prise en passant au coup suivant
            // La position mémorisée est la case que le pion a sautée (case intermédiaire)
            if (piece.PeutCharger() && Math.Abs(dy) == 2 && dx == 0)
                _dernierPionDoubleAvance = (x1, y1 + Math.Sign(dy));

            // Promotion : si un pion atteint la dernière rangée, il devient automatiquement une Dame
            if (piece.PeutEtrePromu())
            {
                int rangeePromotion = piece.PieceEstBlanche ? 7 : 0;
                if (y2 == rangeePromotion)
                    _grillage[x2, y2] = new Dame(piece.PieceEstBlanche, false);
            }
        }

        public bool VerificationEchec(bool estBlanc)
        {
            List<Tuple<int, int>> listePos = TrouverPiecesVulnerables(estBlanc);
            if (listePos.Count <= 0) return false; // Aucune pièce vulnérable (ne devrait pas arriver).
            foreach (Tuple<int, int> pos in listePos)
            {
                int roiCol = pos.Item1;
                int roiRow = pos.Item2;

                for (int col = 0; col < 8; col++)
                {
                    for (int row = 0; row < 8; row++)
                    {
                        Piece piece = _grillage[col, row];
                        if (piece == null || piece.PieceEstBlanche == estBlanc)
                            continue; // Ignorer les cases vides et les pièces alliées

                        // Traitement spécial pour le pion : attaque uniquement en diagonale vers l'avant
                        // Un pion blanc (dirAttaque=+1) attaque (col±1, row+1) ; noir : (col±1, row-1)
                        if (piece is Pion)
                        {
                            int dirAttaque = piece.PieceEstBlanche ? 1 : -1;
                            if (row + dirAttaque == roiRow && Math.Abs(col - roiCol) == 1)
                                return true;
                            continue;
                        }

                        // Pour les autres pièces : vérifier la géométrie (piece.ValiderCoup = niveau pièce, pas plateau)
                        Coup attaque = new Coup((col, row), (roiCol, roiRow), !estBlanc);
                        if (!piece.ValiderCoup(attaque))
                            continue;

                        // Vérifier le chemin libre pour les pièces qui ne peuvent pas sauter
                        if (piece.CauseCollision() && !CheminLibre(attaque))
                            continue;

                        return true;
                    }
                }
            }
            return false;
        }

        // Cherche une liste des positions des pièces vulnérables de la couleur donnée sur le plateau.
        // Retourne (-1, -1) si le Roi est introuvable (cas anormal en partie normale).
        // Utilisé par VerificationEchec et indirectement par VerificationEchecMat et VerificationEchecPat.
        private List<Tuple<int, int>> TrouverPiecesVulnerables(bool estBlanc)
        {
            List<Tuple<int, int>> liste = new List<Tuple<int, int>>();
            for (int col = 0; col < 8; col++)
                for (int row = 0; row < 8; row++)
                {
                    Piece p = _grillage[col, row];
                    if (p != null && p.PieceEstVulnerable() && p.PieceEstBlanche == estBlanc)
                        liste.Add(new Tuple<int,int>(col, row));
                }
            return liste;
        }

        // Retourne une copie du plateau après avoir appliqué le coup, sans modifier le plateau original.
        // Utilisé pour vérifier qu'un coup ne laisse pas son propre roi en échec.
        private Plateau SimulerCoup(Coup coup)
        {
            Plateau copie = new Plateau(this);
            copie.JouerCoup(coup);
            return copie;
        }

        // Maybe good?
        // Génère tous les coups légaux disponibles pour la couleur donnée.
        // Teste les 64×64 paires (source, destination) possibles et filtre par ValiderCoup complet.
        // Coûteux en calcul mais fonctionnel pour un jeu non-optimisé.
        // Utilisé uniquement par VerificationEchecMat et VerificationEchecPat.
        // Retirée pour l'instant, y revenir plus tard.

        // Détecte l'échec et mat : le roi est en échec ET aucun coup légal n'existe pour s'en sortir.
        // Retourne false si le roi n'est pas en échec (condition nécessaire au mat).
        // Délègue à ObtenirTousCoupsPossibles pour énumérer tous les coups légaux.
        public bool VerificationEchecMat(bool estBlanc)
        {
            // TODO: Implémenter via ObtenirTousCoupsPossibles (à réintégrer avec polymorphisme).
            return false;
        }

        // Détecte le pat : le roi n'est PAS en échec mais aucun coup légal n'est disponible (nulle).
        // Retourne false si le roi est en échec (ce serait un mat, pas un pat).
        // Délègue à ObtenirTousCoupsPossibles pour énumérer tous les coups légaux.
        public bool VerificationEchecPat(bool estBlanc)
        {
            // TODO: Implémenter via ObtenirTousCoupsPossibles (à réintégrer avec polymorphisme).
            return false;
        }

        // Destructeur
        ~Plateau() { }

    }
}
=======
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Tp1Echec
{
    public class Plateau
    {

        // Attributs

        // Grille 0-based : _grillage[col, row] avec col 0=a..7=h, row 0=rangée 1..7=rangée 8.
        // Les blancs démarrent rows 0-1, les noirs rows 6-7.
        private Piece[,] _grillage;

        // Mémorise la position du pion qui vient de faire un double avance (pour la prise en passant).
        // Null si le dernier coup n'était pas un double avance de pion.
        // Réinitialisé à null au début de chaque JouerCoup.
        private (int, int)? _dernierPionDoubleAvance;

        // Constructeur

        public Plateau()
        {
            _grillage = new Piece[8, 8];
            _dernierPionDoubleAvance = null;
        }

        public Plateau(Plateau plateau)
        {
            _grillage = new Piece[8, 8];
            _dernierPionDoubleAvance = plateau._dernierPionDoubleAvance;
            for (int i = 0; i < 8; i++)
            {
                for(int j = 0; j < 8; j++)
                {
                    if (plateau._grillage[i, j] != null)
                        _grillage[i, j] = plateau._grillage[i, j].Copier();
                }
            }
        }

        // Indexeur

        private Piece this[int x, int y]
        {
            get { return _grillage[x, y]; }
            set { _grillage[x, y] = value; }
        }

        // Méthodes

        // DONE
        // Initialise le plateau en position de départ standard des échecs.
        // Utilise des indices 0-based : col 0=a..7=h, row 0=rangée 1..7=rangée 8.
        // Les blancs occupent rows 0-1, les noirs rows 6-7.
        // Toutes les pièces reçoivent pieceNaPasBouge=true pour activer les coups spéciaux initiaux (roque, double avance).
        public void InitialiserPlateau()
        {
            // Vider toutes les cases
            for (int col = 0; col < 8; col++)
                for (int row = 0; row < 8; row++)
                    _grillage[col, row] = null;

            // Row 0 : pièces majeures blanches (ordre standard : Tour, Cavalier, Fou, Dame, Roi, Fou, Cavalier, Tour)
            _grillage[0, 0] = new Tour(true, true);
            _grillage[1, 0] = new Cavalier(true, true);
            _grillage[2, 0] = new Fou(true, true);
            _grillage[3, 0] = new Dame(true, true);
            _grillage[4, 0] = new Roi(true, true);
            _grillage[5, 0] = new Fou(true, true);
            _grillage[6, 0] = new Cavalier(true, true);
            _grillage[7, 0] = new Tour(true, true);

            // Row 1 : pions blancs
            for (int col = 0; col < 8; col++)
                _grillage[col, 1] = new Pion(true, true);

            // Rows 2-5 : vides (déjà null après le vidage)

            // Row 6 : pions noirs
            for (int col = 0; col < 8; col++)
                _grillage[col, 6] = new Pion(false, true);

            // Row 7 : pièces majeures noires (même ordre que row 0)
            _grillage[0, 7] = new Tour(false, true);
            _grillage[1, 7] = new Cavalier(false, true);
            _grillage[2, 7] = new Fou(false, true);
            _grillage[3, 7] = new Dame(false, true);
            _grillage[4, 7] = new Roi(false, true);
            _grillage[5, 7] = new Fou(false, true);
            _grillage[6, 7] = new Cavalier(false, true);
            _grillage[7, 7] = new Tour(false, true);

            _dernierPionDoubleAvance = null;
        }

        // DONE
        // Vérifie si des coordonnées 0-based sont dans les limites du plateau 8x8.
        // Doit être appelée avant tout accès à _grillage pour éviter les exceptions d'index hors bornes.
        // Simple vérification d'intervalle : 0 <= x < 8 et 0 <= y < 8.
        public bool PositionDansPlateau(int x, int y)
        {
            return x >= 0 && x < 8 && y >= 0 && y < 8;
        }

        // DONE
        // Sérialise l'état du plateau en texte lisible pour le débogage ou l'export.
        // Chaque case est représentée par Serilization() de la pièce ou "Vide", séparées par "|".
        // Une nouvelle ligne sépare chaque rangée (row 0 à row 7).
        // 32 pièces attendues en position initiale, 32 cases "_".
        public string serilizationPlateau()
        {
            StringBuilder sb = new StringBuilder();
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    if (col > 0) sb.Append(",");
                    Piece p = _grillage[col, row];
                    sb.Append(p != null ? p.Serilization() : "_");
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }

        // FONCTION AvoirPiecesEnnemi
        // Parse le plateau et retourne une List<Piece> de toutes les pieces ennemies
        // Recoit en paramètre une couleur (avec le booléen).
        public List<Piece> AvoirPiecesEnnemi(bool estBlanc)
        {
            List<Piece> piecesEnnemies = new List<Piece>();
            for (int col = 0; col < 8; col++)
                for (int row = 0; row < 8; row++)
                {
                    Piece p = _grillage[col, row];
                    if (p != null && p.PieceEstBlanche != estBlanc)
                        piecesEnnemies.Add(p);
                }
            return piecesEnnemies;
        }

        // Valide un coup complet en appliquant toutes les règles du jeu dans l'ordre.
        // Retourne false dès qu'une règle est violée (validation en court-circuit).
        // Délègue la géométrie à piece.ValiderCoup, le contexte du pion à ValiderCoupPion,
        // le roque à ValiderRoque, et vérifie via SimulerCoup qu'on ne se met pas en échec.
        public bool ValiderCoup(Coup coup)
        {
            int x1 = coup.posDebut.Item1, y1 = coup.posDebut.Item2;
            int x2 = coup.posFin.Item1,  y2 = coup.posFin.Item2;

            // 1. Positions dans les bornes du plateau
            if (!(PositionDansPlateau(x1, y1) && PositionDansPlateau(x2, y2)))
                return false;

            // 2. Une pièce doit être présente à la position de départ
            Piece piece = _grillage[x1, y1];
            if (piece == null)
                return false;

            // 3. La pièce doit appartenir au joueur dont c'est le tour
            if (piece.PieceEstBlanche != coup.estTourBlanc)
                return false;

            // 4. La destination ne doit pas être occupée par une pièce amie (sauf roque : la Tour est alliée)
            Piece destination = _grillage[x2, y2];
            if (destination != null && destination.PieceEstBlanche == coup.estTourBlanc
                && !(piece.PeutInitierRoque() && !piece.PieceABouge() && destination.PeutSuivreRoque() && !destination.PieceABouge()))
                return false;

            int dx = x2 - x1;
            int dy = y2 - y1;

            // 4.5 Roque : si le Roi capture la Tour alliée, déléguer entièrement à ValiderRoque
            if (piece.PeutInitierRoque() && !piece.PieceABouge()
                && destination != null && destination.PeutSuivreRoque() && !destination.PieceABouge())
                return ValiderRoque(coup);

            // 5. Validation géométrique déléguée à la pièce
            if (!piece.ValiderCoup(coup))
                return false;

            // TODO: Gérer les règles contextuelles du Pion (polymorphisme only)

            // 7. Chemin libre pour les pièces qui ne peuvent pas sauter par-dessus d'autres pièces
            if (piece.CauseCollision())
            {
                if (!CheminLibre(coup))
                    return false;
            }

            // TODO: Rétablir la vérification post-coup (SimulerCoup à réimplémenter sans accès aux champs privés).

            return true;
        }

        // Valide les règles contextuelles propres au pion (direction, avance, prise, prise en passant).
        // Le pion est asymétrique : avance droit mais prend en diagonale, avec règles de rangée initiale.
        // Direction : blanc avance vers row+1 (dy>0), noir vers row-1 (dy<0).
        // Cas limites gérés : double avance depuis rangée initiale, prise en passant via _dernierPionDoubleAvance.
        private bool ValiderCoupPion(Coup coup)
        {
            int x1 = coup.posDebut.Item1, y1 = coup.posDebut.Item2;
            int x2 = coup.posFin.Item1,  y2 = coup.posFin.Item2;

            int dx = x2 - x1;
            int dy = y2 - y1;

            // Vérifier la direction : blanc monte (dy>0), noir descend (dy<0)
            int directionAttendue = coup.estTourBlanc ? 1 : -1;
            if (Math.Sign(dy) != directionAttendue)
                return false;

            // Avance d'une case : en ligne droite, destination vide
            if (dx == 0 && Math.Abs(dy) == 1)
            {
                return _grillage[x2, y2] == null;
            }

            // Avance de deux cases : rangée initiale, pièce n'a pas bougé, cases intermédiaire et destination vides
            if (dx == 0 && Math.Abs(dy) == 2)
            {
                Piece pion = _grillage[x1, y1];
                int rowInitial = coup.estTourBlanc ? 1 : 6;
                int yMilieu = y1 + directionAttendue;
                return y1 == rowInitial
                    && pion.PieceNaPasBouge
                    && _grillage[x1, yMilieu] == null
                    && _grillage[x2, y2] == null;
            }

            // Prise diagonale : |dx|==1, |dy|==1
            if (Math.Abs(dx) == 1 && Math.Abs(dy) == 1)
            {
                // Prise normale : pièce ennemie sur la case de destination
                Piece cible = _grillage[x2, y2];
                if (cible != null && cible.PieceEstBlanche != coup.estTourBlanc)
                    return true;

                // Prise en passant : destination vide mais un pion ennemi est sur la colonne cible, même rangée que le pion
                if (_dernierPionDoubleAvance.HasValue)
                {
                    (int epCol, int epRow) = _dernierPionDoubleAvance.Value;
                    // Le pion capturé est en (x2, y1) : même colonne que la destination, même rangée que le pion qui prend
                    if (epCol == x2 && epRow == y1)
                        return true;
                }

                return false;
            }

            return false;
        }

        // DONE
        // Vérifie que toutes les cases entre la position de départ et d'arrivée sont vides.
        // Utilisé pour les pièces à déplacement glissant (Tour, Fou, Dame) qui ne peuvent pas sauter.
        // Le signe de dx/dy détermine la direction du parcours case par case.
        // Les cases de départ et d'arrivée sont exclues (l'arrivée peut être une capture légale).
        private bool CheminLibre(Coup coup)
        {
            int x1 = coup.posDebut.Item1, y1 = coup.posDebut.Item2;
            int x2 = coup.posFin.Item1,  y2 = coup.posFin.Item2;

            int stepX = Math.Sign(x2 - x1);
            int stepY = Math.Sign(y2 - y1);

            int x = x1 + stepX;
            int y = y1 + stepY;

            while (x != x2 || y != y2)
            {
                if (_grillage[x, y] != null)
                    return false;
                x += stepX;
                y += stepY;
            }
            return true;
        }

        // Les méthodes PieceABouge(): bool, PeutInitierRoque(): bool et PeutSuivreRoque(): bool vont te permettre de vérifier si le roque peut être fait.
        // 1. Valide si le roque peut être fait. (SINON early return)
        // 2. Si correct, faire la validation que le chemin pour le roi est Safe (copier plateau, null où le roi est, nouveau roi à la première case intermédiaire, vérifierEchec(), si good, on recommence, jusqu'à atteindre la case destination) early return sinon.
        // 3. Si tout est parfait, on retourne true.
        private bool ValiderRoque(Coup coup)
        {
            int x1 = coup.posDebut.Item1, y1 = coup.posDebut.Item2;
            int x2 = coup.posFin.Item1,  y2 = coup.posFin.Item2;

            // Le roque est horizontal (même rangée)
            if (y1 != y2) return false;

            Piece roi = _grillage[x1, y1];
            Piece tour = _grillage[x2, y2];

            // 1. Vérifications polymorphiques : Roi et Tour n'ont pas bougé
            if (roi == null || !roi.PeutInitierRoque() || roi.PieceABouge()) return false;
            if (tour == null || !tour.PeutSuivreRoque() || tour.PieceABouge()) return false;

            // Chemin libre entre le roi et la tour (cases intermédiaires)
            if (!CheminLibre(coup)) return false;

            // 2. Le roi ne doit pas être en échec sur sa case actuelle, de transit, ni d'arrivée
            // On simule le roi à chaque case traversée (case 0 = départ, 1 = transit, 2 = arrivée finale)
            int direction = Math.Sign(x2 - x1);
            for (int pas = 0; pas <= 2; pas++)
            {
                int caseRoi = x1 + pas * direction;
                Plateau simulation = new Plateau(this);
                simulation._grillage[x1, y1] = null;
                simulation._grillage[caseRoi, y1] = roi;
                if (simulation.VerificationEchec(coup.estTourBlanc))
                    return false;
            }

            // 3. Tout est valide
            return true;
        }

        // Very bad (pas contente).
        // Exécute un coup validé en appliquant tous les effets de bord sur le plateau.
        // Cas spéciaux gérés dans l'ordre : prise en passant, roque (déplacement Tour), déplacement principal,
        // mémorisation du double avance, promotion automatique en Dame à la dernière rangée.
        // SetPieceABouge(true) désactive les coups spéciaux futurs (roque, double avance du pion).
        public void JouerCoup(Coup coup)
        {
            int x1 = coup.posDebut.Item1, y1 = coup.posDebut.Item2;
            int x2 = coup.posFin.Item1,  y2 = coup.posFin.Item2;

            int dx = x2 - x1;
            int dy = y2 - y1;

            Piece piece = _grillage[x1, y1];

            // Réinitialiser la prise en passant : valide seulement pendant le coup suivant
            _dernierPionDoubleAvance = null;

            // TODO: Prise en passant — réimplémenter avec polymorphisme (PriseEnPassant() sur Pion).
            // Le pion capturé est sur (x2, y1) : même colonne que destination, même rangée que pion
            if (piece is Pion pion)
            {
                int direction = pion.PieceEstBlanche ? 1 : -1;

                // Si le pion capture en diagonale mais la destination est vide -> prise en passant
                if (Math.Abs(dx) == 1 && dy == direction && _grillage[x2, y2] == null)
                {
                    _grillage[x2, y1] = null; // retirer le pion capturé
                }

                // Double avance
                if (dx == 0 && Math.Abs(dy) == 2)
                {
                    _dernierPionDoubleAvance = (x2, y2);
                }

                // Promotion automatique
                int derniereLigne = pion.PieceEstBlanche ? 7 : 0;
                if (y2 == derniereLigne)
                {
                    _grillage[x2, y2] = new Dame(pion.PieceEstBlanche, true);
                    _grillage[x1, y1] = null;
                    return; // La promotion remplace le pion, fin du coup
                }
            }


            // TODO: Roque — réimplémenter avec polymorphisme (PeutInitierRoque() / PeutSuivreRoque()).
            // Côté roi (dx=+2) : Tour de col 7 → col 5 ; côté dame (dx=-2) : Tour de col 0 → col 3
            if (piece.PeutInitierRoque() && Math.Abs(x2 - x1) == 2)
            {
                int direction = (x2 - x1) / 2; // +2 ou -2
                int tourXDepart = direction > 0 ? 7 : 0;
                int tourXArrivee = direction > 0 ? 5 : 3;

                Piece tour = _grillage[tourXDepart, y1];
                _grillage[tourXArrivee, y1] = tour;
                _grillage[tourXDepart, y1] = null;
                tour.SetPieceABouge(true);
            }


            // Déplacer la pièce principale
            _grillage[x2, y2] = piece;
            _grillage[x1, y1] = null;
            piece.SetPieceABouge(true);

            // TODO: Mémorisation double avance — réimplémenter avec polymorphisme (DoubleAvance() sur Pion).

            // TODO: Promotion automatique — réimplémenter avec polymorphisme (PeutPromouvoir() sur Pion).
        }

        public bool VerificationEchec(bool estBlanc)
        {
            List<Tuple<int, int>> listePos = TrouverPiecesVulnerables(estBlanc);
            if (listePos.Count <= 0) return false; // Aucune pièce vulnérable (ne devrait pas arriver).
            foreach (Tuple<int, int> pos in listePos)
            {
                int roiCol = pos.Item1;
                int roiRow = pos.Item2;

                for (int col = 0; col < 8; col++)
                {
                    for (int row = 0; row < 8; row++)
                    {
                        Piece piece = _grillage[col, row];
                        if (piece == null || piece.PieceEstBlanche == estBlanc)
                            continue; // Ignorer les cases vides et les pièces alliées

                        // Traitement spécial pour le pion : attaque uniquement en diagonale vers l'avant
                        // Un pion blanc (dirAttaque=+1) attaque (col±1, row+1) ; noir : (col±1, row-1)
                        if (piece is Pion)
                        {
                            int dirAttaque = piece.PieceEstBlanche ? 1 : -1;
                            if (row + dirAttaque == roiRow && Math.Abs(col - roiCol) == 1)
                                return true;
                            continue;
                        }

                        // Pour les autres pièces : vérifier la géométrie (piece.ValiderCoup = niveau pièce, pas plateau)
                        Coup attaque = new Coup((col, row), (roiCol, roiRow), !estBlanc);
                        if (!piece.ValiderCoup(attaque))
                            continue;

                        // Vérifier le chemin libre pour les pièces qui ne peuvent pas sauter
                        if (piece.CauseCollision() && !CheminLibre(attaque))
                            continue;

                        return true;
                    }
                }
            }
            return false;
        }

        // Cherche une liste des positions des pièces vulnérables de la couleur donnée sur le plateau.
        // Retourne (-1, -1) si le Roi est introuvable (cas anormal en partie normale).
        // Utilisé par VerificationEchec et indirectement par VerificationEchecMat et VerificationEchecPat.
        private List<Tuple<int, int>> TrouverPiecesVulnerables(bool estBlanc)
        {
            List<Tuple<int, int>> liste = new List<Tuple<int, int>>();
            for (int col = 0; col < 8; col++)
                for (int row = 0; row < 8; row++)
                {
                    Piece p = _grillage[col, row];
                    if (p != null && p.PieceEstVulnerable() && p.PieceEstBlanche == estBlanc)
                        liste.Add(new Tuple<int,int>(col, row));
                }
            return liste;
        }

        // PAS GOOD.
        private Plateau SimulerCoup(Coup coup)
        {
            return new Plateau(this);
        }

        // Maybe good?
        // Génère tous les coups légaux disponibles pour la couleur donnée.
        // Teste les 64×64 paires (source, destination) possibles et filtre par ValiderCoup complet.
        // Coûteux en calcul mais fonctionnel pour un jeu non-optimisé.
        // Utilisé uniquement par VerificationEchecMat et VerificationEchecPat.
        // Retirée pour l'instant, y revenir plus tard.

        // Détecte l'échec et mat : le roi est en échec ET aucun coup légal n'existe pour s'en sortir.
        // Retourne false si le roi n'est pas en échec (condition nécessaire au mat).
        // Délègue à ObtenirTousCoupsPossibles pour énumérer tous les coups légaux.
        public bool VerificationEchecMat(bool estBlanc)
        {
            // TODO: Implémenter via ObtenirTousCoupsPossibles (à réintégrer avec polymorphisme).
            return false;
        }

        // Détecte le pat : le roi n'est PAS en échec mais aucun coup légal n'est disponible (nulle).
        // Retourne false si le roi est en échec (ce serait un mat, pas un pat).
        // Délègue à ObtenirTousCoupsPossibles pour énumérer tous les coups légaux.
        public bool VerificationEchecPat(bool estBlanc)
        {
            // TODO: Implémenter via ObtenirTousCoupsPossibles (à réintégrer avec polymorphisme).
            return false;
        }

        // Destructeur
        ~Plateau() { }

    }
}
>>>>>>> 74a96098245e3f897f17288eefce4c918bf3c9c3
