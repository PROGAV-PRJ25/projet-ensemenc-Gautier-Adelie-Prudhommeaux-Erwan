public class Plantes {
    public string Nom {get; set;}
    public int[] Position {get; set;}
    public string Nature {get; set;}
    public bool Comestible {get; set;}
    public Saisons Saison {get; set;}
    public string Terrain {get; set;}
    public int Place {get; set;}
    public int[] Besoin {get; set;}  //[eaumin, eaumax, lumièremin, applaudissementmin]
    public int[] EtatActuel {get; set;}  //[vie, eau, lumière, applaudissement]
    public int Longevite {get; set;}  //En mois
    public int Produit {get; set;}
    public List<Maladies> Maladie {get; set;}  //les maladies qu'a la plante
    public double Croissance {get; set;}  //Temps de croissance en mois
    public int Hauteur {get; set;}
    public int Id {get;set;}
    public static int IdSuivant = 1;


    public Plantes(string nom, int[] position, string nature, bool comestible, Saisons saison, string terrain, int place, int[] besoin, int[] etatActuel, int longevite, int produit, List<Maladies> maladie, double croissance, int hauteur) {
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
        IdSuivant ++;
    }
}

//------------------------------------ Plantes cultivables -------------------------------------

public class Etoile : Plantes {
    public Etoile(int[] position) : base("Etoile", position, "Polycarpique", false, new Saisons("Aucune",[0,0,0,0,0,0,0,0,0,0,0,0,0]), "Aucun", 1, [100,150,150,0],[100,125,160,0], 12, 1, new List<Maladies> {}, 1, 3) {}
} //A voir si on enleve les positions (pas utile)

public class Meteorite : Plantes {
    public Meteorite(int[] position) : base("Météorite", position, "Monocarpique", false, new Saison1(), "Petit Prince", 1, [], [150], 24, 0, new List<Maladies> {}, 6, 2) {
        Random aleatoire = new Random();
        Produit = aleatoire.Next(2,6);
    }
}

public class Rose : Plantes {
    public Rose(int[] position) : base("Rose", position, "Monocarpique", false, new Saison3(), "Petit Prince", 1, [], [50], 2, 1, new List<Maladies> {}, 1, 3) {}
}

public class Chapeau : Plantes {
    public Chapeau(int[] position) : base("Chapeau", position, "Monocarpique", true, new Saisons("Aucune",[0,0,0,0,0,0,0,0,0,0,0,0,0]), "Aucun", 1, [], [100], 24, 2, new List<Maladies> {}, 1, 2) {}
}

public class Nuage : Plantes {
    public Nuage(int[] position) : base("Nuage", position, "Polycarpique", true, new Saison2(), "Aucun", 1, [], [100], 72, 3, new List<Maladies> {}, 2, 1) {}
}

public class EtoileFilante : Plantes {
    public EtoileFilante(int[] position) : base("Etoile filante", position, "Monocarpique", false, new Saison1(), "Businessman", 1, [], [50], 2, 1, new List<Maladies> {}, 1, 2) {}
}

public class Alcootier : Plantes {
    public Alcootier(int[] position) : base("Alcootier", position, "Polycarpique", true, new Saison3(), "Buveur", 4, [], [500], 240, 1, new List<Maladies> {}, 8, 5) {}
}

public class PlanteOrgueilleuse : Plantes {
    public PlanteOrgueilleuse(int[] position) : base("Plante orgueilleuse", position, "Polycarpique", false, new Saisons("Aucune",[0,0,0,0,0,0,0,0,0,0,0,0,0]), "Vaniteux", 1, [], [100], 36, 1, new List<Maladies> {}, 0.75, 3) {}
}

public class Couronne : Plantes {
    public Couronne(int[] position) : base("Couronne", position, "Monocarpique", true, new Saisons("Aucune",[0,0,0,0,0,0,0,0,0,0,0,0,0]), "Roi", 1, [], [200], 12, 1, new List<Maladies> {}, 3, 2) {}
}

public class Planete : Plantes {
    public Planete(int[] position) : base("Planète", position, "Monocarpique", false, new Saison2(), "Géographe", 1, [], [1000], 60, 1, new List<Maladies> {}, 24, 3) {}
}

public class Lampadaire : Plantes {
    public Lampadaire(int[] position) : base("Lampadaire", position, "Polycarpique", false, new Saison1(),"Réverbère", 1, [], [100], 1200, 0, new List<Maladies> {}, 2, 4) {}
}


//------------------------------------ Plantes nuisibles -------------------------------------

public class Baobab : Plantes {
    public List<int> TauxApparition {get; set;}
    public Baobab(int[] position) : base("Baobab", position, "Monocarpique", false, new Saisons("Aucune",[0,0,0,0,0,0,0,0,0,0,0,0,0]), "Aucun", 4, [], [700], 240, 0, new List<Maladies> {}, 5, 5) {
        TauxApparition = new List<int> {0, 0, 0, 0, 0, 0, 0};
    }
}

public class Champignon : Plantes {
    public List<int> TauxApparition {get; set;}
    public Champignon(int[] position) : base("Champignon", position, "Monocarpique", false, new Saisons("Aucune",[0,0,0,0,0,0,0,0,0,0,0,0,0]), "Aucun", 1, [], [100], 12, 0, new List<Maladies> {}, 1, 1) {
        TauxApparition = new List<int> {0, 0, 0, 0, 0, 0, 0};
    }
}