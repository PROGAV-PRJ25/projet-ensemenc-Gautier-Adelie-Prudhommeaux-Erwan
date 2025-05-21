public class Animaux
{
    public string Nom { get; set; }
    public int[] Position { get; set; }  //[ligne, colonne]
    public int PlaceOccupée { get; set; }
    public int Groupe { get; set; }
    public List<int> TauxApparition { get; set; }
    public int Id { get; set; }
    public static int IdSuivant = 1;
    public Jardin Jardin { get; set; }
    public string Emoji { get; set; }

    public Animaux(string nom, int[] position, int placeOccupée, int groupe, List<int> tauxApparition, Jardin jardin, string emoji)
    {
        Nom = nom;
        Position = position;
        PlaceOccupée = placeOccupée;
        Groupe = groupe;
        TauxApparition = tauxApparition;
        Id = IdSuivant;
        IdSuivant++;
        Jardin = jardin;
        Emoji = emoji;
    }
}


//------------------------------------ Animaux amicaux -------------------------------------

public class Serpent : Animaux
{
    public bool Caché { get; set; }
    public Serpent(int[] position, Jardin jardin) : base("Serpent", position, 1, 1, new List<int> { 0, 0, 0, 0, 0, 0, 0 }, jardin,"🐍")
    {
        Caché = false;
    }

    public void Deplacer()
    {
        //Si elephant sur une case adjacente, il va le manger
        //Si un chapeau sur une case adjacente, il va se cacher en dessous
        //Sinon, se dirige vers l'elephant ou le chapeau le plus proche, ou se déplace aléatoirement
    }
}

public class Mouton : Animaux
{
    public Mouton(int[] position, Jardin jardin) : base("Mouton", position, 1, 0, new List<int> { 0, 0, 0, 0, 0, 0, 0 }, jardin, "🐑")
    {
        Random aleatoire = new Random();
        Groupe = aleatoire.Next(1, 3);
    }

    public void Deplacer()
    {
        //Si baobab sur une case adjacente, il va le manger
        //Sinon, se dirige vers le baobab ou le mouton le plus proche, ou se déplace aléatoirement
        Plantes planteEcrasée = Jardin.RechercherPlante(Position);
        if (planteEcrasée.Nom =="Baobab") {
            Jardin.SupprimerPlante(planteEcrasée.Position);
        }
        else {
            int[] coPlanteProche = [Position[0], Position[1]];
            double distPlanteProche = 1000;
            for (int i = 0; i < 21; i++)
            {
                for (int j = 0; j < 21; j++)
                {
                    if (Jardin.MatPlante[i, j] > 0)
                    {
                        double dist = Math.Sqrt(Math.Pow(Position[0] - i, 2) + Math.Pow(Position[1] - j, 2));
                        if (dist < distPlanteProche)
                        {
                            distPlanteProche = dist;
                            coPlanteProche = [i, j];
                        }
                    }
                }
            }
            int[] deplacement = Jardin.DeplacementDirigéAnimaux( Position, coPlanteProche);
            Jardin.MatAnimaux[Position[0], Position[1]] = 0;
            Position[0] += deplacement[0];
            Position[1] += deplacement[1];
            Jardin.MatAnimaux[Position[0], Position[1]] = Id;
        }
    }
}


//------------------------------------ Animaux nuisibles -------------------------------------

public class Elephant : Animaux
{
    public int[] Dégats { get; set; }
    public int Direction { get; set; }
    //La position correspond à celle de la tête de l'éléphant (qui fait 2 cases de long)
    public Elephant(int[] position, Jardin jardin) : base("Éléphant", position, 2, 1, new List<int> { 0, 0, 0, 0, 0, 0, 0 }, jardin, "🐘")
    {
        Dégats = [-2000, 0, 0, 0];
        Random aleatoire = new Random();
        Direction = aleatoire.Next(4);
        //0:N  1:S, 2:O, 3:E
    }

    public void Deplacer()
    {
        //Se déplace en ligne droite
        if (Direction == 0)
        {
            if (Position[0] - 1 < 0 || (Jardin.MatAnimaux[Position[0] - 1, Position[1]] == 0))
            {
                Jardin.SupprimerAnimaux(Position);
            }
            else if (Jardin.MatAnimaux[Position[0] - 1, Position[1]] == -1)
            {
                Jardin.MatAnimaux[Position[0] + 1, Position[1]] = 0;   //On supprime l'arrière de l'éléphant
                Position[0] = Position[0] - 1;
                Jardin.MatAnimaux[Position[0], Position[1]] = Id;
                Jardin.SupprimerPlante(Position);
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
                Jardin.MatAnimaux[Position[0] - 1, Position[1]] = 0;   //On supprime l'arrière de l'éléphant
                Position[0] = Position[0] + 1;
                Jardin.MatAnimaux[Position[0], Position[1]] = Id;
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
                Jardin.MatAnimaux[Position[0], Position[1] + 1] = 0;   //On supprime l'arrière de l'éléphant
                Position[1] = Position[1] - 1;
                Jardin.MatAnimaux[Position[0], Position[1]] = Id;
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
                Jardin.MatAnimaux[Position[0], Position[1] - 1] = 0;   //On supprime l'arrière de l'éléphant
                Position[1] = Position[1] + 1;
                Jardin.MatAnimaux[Position[0], Position[1]] = Id;
                Jardin.SupprimerPlante(Position);
            }
        }
    }
}


public class Oiseau : Animaux
{
    public int[] Dégats { get; set; }
    public Oiseau(int[] position, Jardin jardin) : base("Oiseau", position, 1, 0, new List<int> { 0, 0, 0, 0, 0, 0, 0 }, jardin, "🐦")
    {
        Random aleatoire = new Random();
        Groupe = aleatoire.Next(2, 4);
        Dégats = [-5, 0, 0, 0];
    }

    public void Deplacer()
    {
        //Si plante sur une case adjacente, il va la picorer --> enlève des pv à la plante
        //Sinon, se dirige vers la plante la plus proche (ou reste sur place si la plante n'est pas accessible)
        if (Jardin.MatPlante[Position[0], Position[1]] > 0) {
            Plantes plantePicorée = Jardin.RechercherPlante(Position);
            for (int i=0; i<4; i++) {
                plantePicorée.EtatActuel[i] += Dégats[i];
            }
        }

        else {
            int[] coPlanteProche = [Position[0], Position[1]];
            double distPlanteProche = 1000;
            for (int i = 0; i < 21; i++)
            {
                for (int j = 0; j < 21; j++)
                {
                    if (Jardin.MatPlante[i, j] > 0)
                    {
                        double dist = Math.Sqrt(Math.Pow(Position[0] - i, 2) + Math.Pow(Position[1] - j, 2));
                        if (dist < distPlanteProche)
                        {
                            distPlanteProche = dist;
                            coPlanteProche = [i, j];
                        }
                    } 
                }
            }
            int[] deplacement = Jardin.DeplacementDirigéAnimaux( Position, coPlanteProche);
            Jardin.MatAnimaux[Position[0], Position[1]] = 0;
            Position[0] += deplacement[0];
            Position[1] += deplacement[1];
            Jardin.MatAnimaux[Position[0], Position[1]] = Id;
        }

    }
}