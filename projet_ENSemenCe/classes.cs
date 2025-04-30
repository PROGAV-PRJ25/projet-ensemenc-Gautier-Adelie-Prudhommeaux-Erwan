public class Plantes {
    public string Nom {get; set;}
    public int[,] Position {get; set;}
    public string Nature {get; set;}
    public bool Comestible {get; set;}
    public Saisons Saison {get; set;}
    public Terrains Terrain {get; set;}
    public int Place {get; set;}
    public List<Besoins> Besoin {get; set;}
    public int Longevite {get; set;}  //En mois
    public int Produit {get; set;}
    public Maladies Maladie {get; set;}
    public int Croissance {get; set;}  //Temps de croissance en mois
    public int Sante {get; set;}
    public int Hauteur {get; set;}


    public Plantes(string nom, int[,] position, string nature, bool comestible, Saisons saison, Terrains terrain, int place, Besoins besoin, int longevite, int produit, Maladies maladie, int croissance, int sante, int hauteur) {
        Nom = nom;
        Position = position;
        Nature = nature;
        Comestible = comestible;
        Saison = saison;
        Terrain = terrain;
        Place = place;
        Besoin = besoin;
        Longevite = longevite;
        Produit = produit;
        Maladie = maladie;
        Croissance = croissance;
        Sante = sante;
        Hauteur = hauteur;
    }
}

public class Etoile : Plantes {
    public Etoile(int[,] position) : base("Etoile", position, "Polycarpique", false, new Saisons(0), new Terrains(0), 1, new Besoins(), 12, 1, new Maladies(), 1, 100, 3) {}
}

public class Meteorite : Plantes {
    public Meteorite(int[,] position) : base("Météorite", position, "Monocarpique", false, new Saisons(1), new Terrains(1), 1, new Besoins(), 24, 0, new Maladies(), 6, 150, 2) {
        Random aleatoire = new Random();
        Produit = aleatoire.Next(2,6);
    }
}

public class Rose : Plantes {
    public Rose(int[,] position) : base("Rose", position, "Monocarpique", false, new Saisons(3), new Terrains(1), 1, new Besoins(), 2, 1, new Maladies(), 1, 50, 3) {}
}

public class Chapeau : Plantes {
    public Chapeau(int[,] position) : base("Chapeau", position, "Monocarpique", true, new Saisons(0), new Terrains(0), 1, new Besoins(), 24, 2, new Maladies(), 1, 100, 2) {}
}

public class Nuage : Plantes {
    public Nuage(int[,] position) : base("Nuage", position, "Polycarpique", true, new Saisons(2), new Terrains(0), 1, new Besoins(), 72, 3, new Maladies(), 2, 100, 1) {}
}

public class EtoileFilante : Plantes {
    public EtoileFilante(int[,] position) : base("Etoile filante", position, "Monocarpique", false, new Saisons(1), new Terrains(2), 1, new Besoins(), 2, 1, new Maladies(), 1, 50, 2) {}
}

public class Alcootier : Plantes {
    public Alcootier(int[,] position) : base("Alcootier", position, "Polycarpique", true, new Saisons(3), new Terrains(3), 4, new Besoins(), 240, 1, new Maladies(), 8, 500, 5) {}
}

public class PlanteOrgueilleuse : Plantes {
    public PlanteOrgueilleuse(int[,] position) : base("Plante orgueilleuse", position, "Polycarpique", false, new Saisons(0), new Terrains(4), 1, new Besoins(), 36, 1, new Maladies(), 0.75, 100, 3) {}
}

public class Couronne : Plantes {
    public Couronne(int[,] position) : base("Couronne", position, "Monocarpique", true, new Saisons(0), new Terrains(5), 1, new Besoins(), 12, 1, new Maladies(), 3, 200, 2) {}
}

public class Planete : Plantes {
    public Planete(int[,] position) : base("Planète", position, "Monocarpique", false, new Saisons(2), new Terrains(6), 1, new Besoins(), 60, 1, new Maladies(), 24, 1000, 3) {}
}

public class Lampadaire : Plantes {
    public Lampadaire(int[,] position) : base("Lampadaire", position, "Polycarpique", false, new Saisons(1), new Terrains(7), 1, new Besoins(), 1200, 0, new Maladies(), 2, 100, 4) {}
}