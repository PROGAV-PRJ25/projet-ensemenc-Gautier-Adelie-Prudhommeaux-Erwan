public class Terrains{
    public string TypeSol {get;set;}
    public string Nomplanete {get;set;}
    public List<Maladies>? MaladiesPossible {get; set;}
    public List<Plantes>? PlanteAchetable {get;set;}
    public Météos? MeteoAjouté {get;set;} 


    public Terrains(int numplanete, List<Maladies> maladiesPossible, List<Plantes> planteAchetable, Météos meteoAjouté){
        T = ["Aucun","Petit Prince","Businessman","Buveur","Vaniteux","Roi","Géographe","Réverbère"];
        Nomplanete = T[numplanete];
        MaladiesPossible = maladiesPossible;
        PlanteAchetable = planteAchetable;
        MeteoAjouté = meteoAjouté;
    }

}
public class TerrainPetitPrince:Terrains{
    public TerrainPetitPrince():base(1,new list<Maladies> {},new list<plantes> {},new Meteos Meteos(0)){}
}
public class TerrainBusinessman:Terrains{
    public TerrainBusinessman():base(2,new list<Maladies> {},new list<plantes> {},new Meteos Meteos(1)){}
}
public class TerrainBuveur:Terrains{
    public TerrainBuveur():base(3,new list<Maladies> {},new list<plantes> {},new Meteos Meteos(3)){}
}
public class TerrainRoi:Terrains{
    public TerrainRoi():base(4,new list<Maladies> {},new list<plantes> {},new Meteos Meteos(4)){}
}
public class TerrainGéographe:Terrains{
    public TerrainGéographe():base(5,new list<Maladies> {},new list<plantes> {},new Meteos Meteos(5)){}
}
public class TerrainRéverbère:Terrains{
    public TerrainRéverbère():base(6,new list<Maladies> {},new list<plantes> {},new Meteos Meteos(6)){}
}


public class Meteos{// a voir comment la faire 
    public Saisons Saison {get;set;} // faire classe saison a voir si on fait classe catastrophe qui serons liéeq
    public string temp {get;set;}
    public List<string> CatastrophePossible {get;set;}// a voir si on fait une classe catastrophe 
    public int NuméroCata {get;set;} // 0 pas de catastrrphe sinon numéro de la catastrophes a definir pour chaque
    public Meteos(int numplanete){
         Saison = new Saisons(0);
         temp = "Calme";

    }
}

public class Jardin{
    public MatrixNode Terrains {get;set;} // définir a chaque numéro une plante ou un animal ou objet
    public List<Plantes> PlantesJouable {get;set;}
    public List<Maladies> MaladiesPossible {get;set;}
    public string[] ObjectsAchetable {get;set;} 
    public int[] Objects {get;set;} // chaque indice correspond a un object. il y en a 23 
    public int[] GrainesDisponibles {get;set;} // chaque indice est assosier a une plante. il y en a 11 
    public int TourActuel {get;set;}
    public int Argent {get;set;}


    public Jardin(){
        TourActuel = 0;
        Argent = 0;
        PlantesJouable = new List<Plantes> {new Etoile(), new Meteorite(), new Rose(), new Chapeau(), new Nuage()};
        MaladiesPossible = new List<Maladies> {new Maladies1};
        ObjectsAchetable = ["Lanterne","Pelle","Écharpe","Pare à vent","Clôture","Tuyaux d'arrosage","Épouventails","Haut parleur"];
        Objects = [0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0];
        GrainesDisponibles = [10,0,0,0,0,0,0,0,0,0,0];
        int[,] mat = new int[7,7];
        Terrains = new MatrixNode(mat , 1);
    }
}


public class MatrixNode
{
    public int[,] Matrix { get; set; }
    public Terrains planete {get;set;}
    public MatrixNode Nord { get; set; }
    public MatrixNode Sud { get; set; }
    public MatrixNode Est  { get; set; }
    public MatrixNode Ouest  { get; set; }

    public MatrixNode(int[,] matrix, int numplanete)
    {
        Matrix = matrix;
        T = ["Aucun","Petit Prince","Businessman","Buveur","Vaniteux","Roi","Géographe","Réverbère"];// a adapter a chaque terrains
        planete = T[numplanete];
    }
}

public void AddMatrix(MatrixNode origin, int[,] newMatrix, string direction, int numplanete)
{
    MatrixNode newNode = new MatrixNode(newMatrix,numplanete);

    switch (direction.ToLower())
    {
        case "Nord":
            origin.Nord = newNode;
            newNode.Sud = origin;
            break;
        case "Sud":
            origin.Sud = newNode;
            newNode.Nord = origin;
            break;
        case "Est":
            origin.Est = newNode;
            newNode.Ouest = origin;
            break;
        case "Ouest":
            origin.Ouest = newNode;
            newNode.Est = origin;
            break;
        default:
            throw new ArgumentException("Direction invalide");
    }
}

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