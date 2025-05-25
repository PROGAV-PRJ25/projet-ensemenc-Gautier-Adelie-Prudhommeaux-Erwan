public class Animaux
{
    public string Nom { get; set; }
    public int[] Position { get; set; }  //Position de l'animal dans la matrice MatAnimaux ([ligne, colonne])
    public int PlaceOccupee { get; set; }  //Le nombre de case que prend l'animal sur le plateau
    public int Id { get; set; }  //Identifiant unique qui caractérise l'animal, généré automatiquement
    public static int IdSuivant = 1;
    public Jardin Jardin { get; set; }
    public string Emoji { get; set; }  //Image de l'animal sur le plateau

    public Animaux(string nom, int[] position, int placeOccupee, Jardin jardin, string emoji)
    {
        Nom = nom;
        Position = position;
        PlaceOccupee = placeOccupee;
        Id = IdSuivant;
        IdSuivant++;
        Jardin = jardin;
        Emoji = emoji;
    }

    public virtual void Deplacer(){}
}


//------------------------------------ Animaux amicaux -------------------------------------

public class Serpent : Animaux
{
    public bool Cacher { get; set; }  //true si le serpent est caché par un chapeau
    public Serpent(int[] position, Jardin jardin) : base("Serpent", position, 1, jardin,"🐍")
    {
        Cacher = false;
    }

    public override void Deplacer()
    {
        //Si un éléphant sur sa case, il va le manger
        //Si un chapeau sur sa case, il va se cacher en dessous
        //Sinon, se dirige vers l'élephant le plus proche, ou reste sur place

        int[] coAnimalProche = Jardin.RechercherAnimalProche(Position, "Éléphant");
        //On recherche l'éléphant le plus proche du serpent
        if (Math.Abs(coAnimalProche[0] - Position[0]) == 1 || Math.Abs(coAnimalProche[1] - Position[1]) == 1)
        {
            //Si l'éléphant trouvé est sur la case d'à côté, on le supprime et on déplace le serpent
            Jardin.SupprimerAnimaux(coAnimalProche);
            Jardin.MatAnimaux[Position[0], Position[1]] = -1;
            Position[0] = coAnimalProche[0];
            Position[1] = coAnimalProche[1];
            Jardin.MatAnimaux[Position[0], Position[1]] = Id;
        }
        else
        {
            //Sinon, on déplace le serpent dans la direction des coordonnées renvoyées par la fonction coAnimalProche (en cherchant un serpent)
            int[] deplacement = Jardin.DeplacementDirigeAnimaux(Position, coAnimalProche);
            Jardin.MatAnimaux[Position[0], Position[1]] = -1;
            Position[0] += deplacement[0];
            Position[1] += deplacement[1];
            Jardin.MatAnimaux[Position[0], Position[1]] = Id;
        }

        if (Jardin.RechercherPlante(Position).Nom == "Chapeau") { Cacher = true; }
        //On regarde si le serpent est sur la même case qu'un chapeau pour se cacher
        else { Cacher = false; }        
    }
}

public class Mouton : Animaux
{
    public Mouton(int[] position, Jardin jardin) : base("Mouton", position, 1, jardin, "🐑") {}

    public override void Deplacer()
    {
        //Si baobab sur sa case, il va le manger
        //Sinon, se dirige vers le baobab le plus proche, ou reste sur place
        Plantes planteEcrasee = Jardin.RechercherPlante(Position);
        if (planteEcrasee.Nom == "Baobab") {
            //Si le mouton est sur un baobab, on le supprime (et le mouton reste sur place)
            Jardin.SupprimerPlante(planteEcrasee.Position);
        }
        else {
            //Sinon, le mouton se dirige vers le baobab le plus proche
            int[] coPlanteProche = Jardin.RechercherPlanteProche(Position, "Baobab");
            int[] deplacement = Jardin.DeplacementDirigeAnimaux(Position, coPlanteProche);
            Jardin.MatAnimaux[Position[0], Position[1]] = -1;
            Position[0] += deplacement[0];
            Position[1] += deplacement[1];
            Jardin.MatAnimaux[Position[0], Position[1]] = Id;
        }
    }
}


//------------------------------------ Animaux nuisibles -------------------------------------

public class Elephant : Animaux
{
    public int[] Degats { get; set; }
    public int Direction { get; set; }
    //La position correspond à celle de la tête de l'éléphant (qui fait 2 cases de long)
    public Elephant(int[] position, Jardin jardin) : base("Éléphant", position, 2, jardin, "🐘")
    {
        Degats = [-2000, 0, 0, 0];
        Random aleatoire = new Random();
        Direction = aleatoire.Next(0, 4);
        Jardin.SupprimerPlante(Position);
        //On choisi aléatoirement la direction de déplacement de l'éléphant
        //0:N  1:S, 2:O, 3:E
    }

    public override void Deplacer()
    {
        //Se déplace en ligne droite, et écrase tout sur son passage
        if (Direction == 0)
        {
            if (Position[0] - 1 < 0 || (Jardin.MatAnimaux[Position[0] - 1, Position[1]] == 0))
            //Si l'éléphant sort des limites du plateau de jeu, on le supprime
            {
                Jardin.SupprimerAnimaux(Position);
            }
            else if (Jardin.MatAnimaux[Position[0] - 1, Position[1]] == -1)
            {
                Jardin.MatAnimaux[Position[0] + 1, Position[1]] = -1;   //On supprime l'arrière de l'éléphant
                Jardin.MatAnimaux[Position[0] - 1, Position[1]] = Id;   //On déplace d'une case la tête de l'éléphant
                Position = [Position[0]-1,Position[1]];
                Jardin.SupprimerPlante(Position);   //On supprime les plantes écrasées
            }
        }

        else if (Direction == 1)
        {
            if (Position[0] + 1 > 20 || (Jardin.MatAnimaux[Position[0] + 1, Position[1]] == 0))
            {
                Jardin.SupprimerAnimaux(Position);
            }
            else if (Jardin.MatAnimaux[Position[0] + 1, Position[1]] == -1)
            {
                Jardin.MatAnimaux[Position[0] - 1, Position[1]] = -1;   //On supprime l'arrière de l'éléphant
                Jardin.MatAnimaux[Position[0] + 1, Position[1]] = Id;
                Position[0] += 1;
                Jardin.SupprimerPlante(Position);
            }
        }
        else if (Direction == 2)
        {
            if (Position[1] - 1 < 0 || (Jardin.MatAnimaux[Position[0], Position[1] - 1] == 0))
            {
                Jardin.SupprimerAnimaux(Position);
            }
            else if (Jardin.MatAnimaux[Position[0], Position[1] - 1] == -1)
            {
                Jardin.MatAnimaux[Position[0], Position[1] + 1] = -1;   //On supprime l'arrière de l'éléphant
                Jardin.MatAnimaux[Position[0], Position[1]-1] = Id;
                Position[1] -= 1;
                Jardin.SupprimerPlante(Position);
            }
        }
        else {
            if (Position[1] + 1 > 20 || (Jardin.MatAnimaux[Position[0], Position[1] + 1] == 0))
            {
                Jardin.SupprimerAnimaux(Position);
            }
            else if (Jardin.MatAnimaux[Position[0], Position[1] + 1] == -1)
            {
                Jardin.MatAnimaux[Position[0], Position[1] - 1] = -1;   //On supprime l'arrière de l'éléphant
                Jardin.MatAnimaux[Position[0], Position[1]] = Id;
                Position[1] += 1;
                Jardin.SupprimerPlante(Position);
            }
        }
    }
}


public class Oiseau : Animaux
{
    public int[] Degats { get; set; }
    public Oiseau(int[] position, Jardin jardin) : base("Oiseau", position, 1, jardin, "🐦")
    {
        //Les dégats que l'oiseau fait à une plante lorsqu'il la picore
        Degats = [-1, 0, 0, 0];
    }

    public override void Deplacer()
    {
        //Si plante sur une case adjacente, il va la picorer --> enlève des pv à la plante
        //Sinon, se dirige vers la plante la plus proche (ou reste sur place si la plante n'est pas accessible)
        if (Jardin.MatPlante[Position[0], Position[1]] == 1) { Jardin.MatPlante[Position[0], Position[1]] = -1; }
        if (Jardin.MatPlante[Position[0], Position[1]] > 0)
        {
            Plantes plantePicoree = Jardin.RechercherPlante(Position);
            if (!plantePicoree.Proteger)
            //L'oiseau fait des dégâts à la plante si celle-ci n'est pas protégée par le joueur
            {
                for (int i = 0; i < 4; i++)
                {
                    plantePicoree.EtatActuel[i] += Degats[i];
                }
            }
        }

            else
            {
                //S'il n'y a pas de plante sous l'oiseau, on recherche la plus proche
                int[] coPlanteProche = Jardin.RechercherPlanteProche(Position, "");
                int[] deplacement = Jardin.DeplacementDirigeAnimaux(Position, coPlanteProche);
                Jardin.MatAnimaux[Position[0], Position[1]] = -1;
                Position[0] += deplacement[0];
                Position[1] += deplacement[1];
                Jardin.MatAnimaux[Position[0], Position[1]] = Id;
            }

    }
}