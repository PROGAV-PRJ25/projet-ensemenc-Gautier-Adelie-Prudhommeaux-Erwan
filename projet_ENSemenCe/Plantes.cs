public abstract class Plantes
{
    public string Nom { get; set; }
    public int[] Position { get; set; }  //Les coordonnées de la plante sur le plateau (dans MatPlantes)
    public string Nature { get; set; }      //Monocarpique ou polycarpique
    public bool Comestible { get; set; }
    public Saisons Saison { get; set; }     //La saison préférée de la plante
    public string Terrain { get; set; }     //Le terrain prégéré de la plante
    public int Place { get; set; }      //Le nombre de case prisent par la plante
    public int[] Besoin { get; set; }  //Liste des besoins de la plante [eaumin, eaumax, lumièremin, applaudissementmin]
    public int[] EtatActuel { get; set; }  //Quantités de besoins que la plante a actuellement [vie, eau, lumière, applaudissement]
    public int Longevite { get; set; }  //Longévité en mois
    public int Produit { get; set; }   //Le nombre de fruits produit par la plante
    public List<Maladies> Maladie { get; set; }  //les maladies qu'a la plante
    public double Croissance { get; set; }  //Temps de croissance en mois
    public int Hauteur { get; set; }
    public int Id { get; set; }  //Identifiant unique caractérisant la plante sur le plateau MatPlante (généré automatiquement)
    public static int IdSuivant = 2;
    public Jardin Jardin { get; set; }
    public string Emoji { get; set; }  //L'image de la plante dans la console
    public bool Proteger { get; set; }
    public bool Explosion { get; set; }
    public int IdFruit { get; set; }  //L'id correspondant à la plante dans la liste des objets possédés par le joueur
    public int JourPlanter { get; set; }  //Le jour de la simluation auquelle la plante a été plantée

    public Plantes(string nom, int[] position, string nature, bool comestible, Saisons saison, string terrain, int place, int[] besoin, int[] etatActuel, int longevite, int produit, List<Maladies> maladie, double croissance, int hauteur, string emoji, Jardin jardin, int idFruit)
    {
        JourPlanter = 0;
        Nom = nom;
        Position = position;
        Nature = nature;
        Comestible = comestible;
        Saison = saison;
        Terrain = terrain;
        Place = place;
        Besoin = besoin;
        EtatActuel = etatActuel;
        Longevite = longevite;
        Produit = produit;
        Maladie = maladie;
        Croissance = croissance;
        Hauteur = hauteur;
        Id = IdSuivant;
        IdSuivant++;
        Emoji = emoji;
        Proteger = false;
        Jardin = jardin;
        Explosion = false;
        IdFruit = idFruit;
        JourPlanter = 0;
    }
    public virtual void Deplacer(){}
}

//------------------------------------ Plante vide -------------------------------------
public class PlanteVide : Plantes {
    public PlanteVide(Jardin jardin) : base("", [], "", false, new Saison1(), "", 0, [0, 0, 0, 0], [0, 0, 0, 0], 0, 0, new List<Maladies> {}, 0, 0,"  ", jardin, -1) {
        Id =-1;
    }
}

//------------------------------------ Plantes cultivables -------------------------------------
public class Etoile : Plantes
{
    public Etoile(int[] position, Jardin jardin) : base("Etoile", position, "Polycarpique", false, new Saisons("Aucune", [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0]), "Aucun", 1, [100, 150, 150, 0], [100, 125, 160, 0], 12, 1, new List<Maladies> { }, 1, 3, "⭐", jardin, 8) { }
} 

public class Meteorite : Plantes {
    public Meteorite(int[] position, Jardin jardin) : base("Météorite", position, "Monocarpique", false, new Saison1(), "Petit Prince", 1, [100, 150, 150, 0], [150, 125, 160, 0], 24, 0, new List<Maladies> { }, 6, 2, "☄️", jardin, 9)
    {
        Random aleatoire = new Random();
        Produit = aleatoire.Next(2, 6);
        //La quantité de fruits produits est tirée aléatoirement
    }
}

public class Rose : Plantes {
    public Rose(int[] position, Jardin jardin) : base("Rose", position, "Monocarpique", false, new Saison3(), "Petit Prince", 1, [100, 150, 150, 0], [50, 125, 160, 0], 2, 1, new List<Maladies> {}, 1, 3,"🌹", jardin, 10) {}
}

public class Chapeau : Plantes {
    public Chapeau(int[] position, Jardin jardin) : base("Chapeau", position, "Monocarpique", true, new Saisons("Aucune",[0,0,0,0,0,0,0,0,0,0,0,0,0]), "Aucun", 1, [100, 150, 150, 0], [100, 125, 160, 0], 24, 2, new List<Maladies> {}, 1, 2,"👒", jardin, 11) {}
}

public class Nuage : Plantes {
    public Nuage(int[] position, Jardin jardin) : base("Nuage", position, "Polycarpique", true, new Saison2(), "Aucun", 1, [100, 150, 150, 0], [100, 125, 160, 0], 72, 3, new List<Maladies> {}, 2, 1,"☁️", jardin, 12) {}
}

public class EtoileFilante : Plantes
{
    public EtoileFilante(int[] position, Jardin jardin) : base("Etoile filante", position, "Monocarpique", false, new Saison1(), "Businessman", 1, [100, 150, 150, 0], [50, 125, 160, 0], 2, 1, new List<Maladies> { }, 1, 2,"💫", jardin, 13) { }

    public override void Deplacer()
    {
        //Les étoiles filantes peuvent se déplacer sur le plateau. Elles se déplacent aléatoirement, s'il n'y a pas d'autre plante sur la case où elle veut aller (sinon, elle reste sur place)
        Random aleatoire = new Random();
        int deplacementX = aleatoire.Next(-1, 2);
        int deplacementY = aleatoire.Next(-1, 2);
        Jardin.MatPlante[Position[0], Position[1]] = -1;
        while (Jardin.MatPlante[Position[0] + deplacementX, Position[1] + deplacementY] != -1)
        {
            deplacementX = aleatoire.Next(-1, 2);
            deplacementY = aleatoire.Next(-1, 2);
        }
        Jardin.MatPlante[Position[0], Position[1]] = 1;
        //L'étoile filante laisse de la poussière d'étoiles derrière elle
        Position[0] += deplacementX;
        Position[1] += deplacementY;
        Jardin.MatPlante[Position[0], Position[1]] = Id;
    }
}

public class Alcootier : Plantes {
    public Alcootier(int[] position, Jardin jardin) : base("Alcootier", position, "Polycarpique", true, new Saison3(), "Buveur", 4, [100, 150, 150, 0], [500, 125, 160, 0], 240, 1, new List<Maladies> {}, 8, 5,"🌳", jardin, 14) {}
}

public class PlanteOrgueilleuse : Plantes {
    public PlanteOrgueilleuse(int[] position, Jardin jardin) : base("Plante orgueilleuse", position, "Polycarpique", false, new Saisons("Aucune",[0,0,0,0,0,0,0,0,0,0,0,0,0]), "Vaniteux", 1, [100, 150, 150, 100], [100, 125, 160, 100], 36, 1, new List<Maladies> {}, 0.75, 3,"🌻", jardin, 15) {}
}

public class Couronne : Plantes {
    public Couronne(int[] position, Jardin jardin) : base("Couronne", position, "Monocarpique", true, new Saisons("Aucune",[0,0,0,0,0,0,0,0,0,0,0,0,0]), "Roi", 1, [100, 150, 150, 0], [200, 125, 160, 0], 12, 1, new List<Maladies> {}, 3, 2,"👑", jardin, 16) {}
}

public class Planete : Plantes {
    public Planete(int[] position, Jardin jardin) : base("Planète", position, "Monocarpique", false, new Saison2(), "Géographe", 1, [100, 150, 150, 0], [1000, 125, 160, 0], 60, 1, new List<Maladies> {}, 24, 3,"🪐", jardin, 17) {}
}

public class Lampadaire : Plantes {
    public Lampadaire(int[] position, Jardin jardin) : base("Lampadaire", position, "Polycarpique", false, new Saison1(),"Réverbère", 1, [100, 150, 150, 0], [100, 125, 160, 0], 1200, 0, new List<Maladies> {}, 2, 4,"🏮", jardin, -1) {}
}


//------------------------------------ Plantes nuisibles -------------------------------------

public class Baobab : Plantes {
    public Baobab(int[] position, Jardin jardin) : base("Baobab", position, "Monocarpique", false, new Saisons("Aucune",[0,0,0,0,0,0,0,0,0,0,0,0,0]), "Aucun", 4, [2000, 150, 150, 0], [700, 125, 160, 0], 240, 0, new List<Maladies> {}, 5, 5,"🌴", jardin, -1) {
    }
}

public class Champignon : Plantes {
    public Champignon(int[] position, Jardin jardin) : base("Champignon", position, "Monocarpique", false, new Saisons("Aucune",[0,0,0,0,0,0,0,0,0,0,0,0,0]), "Aucun", 1, [100, 150, 150, 0], [100, 125, 160, 0], 12, 0, new List<Maladies> {}, 1, 1,"🍄", jardin, -1) {
    }
}