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


public class Meteos{
    public string Nom {get;set;}
    public int[] SaisonPourcent  {get;set;}// pourcentage de possibilité pour les 3 saisons
    public int NuméroCata {get;set;} // 0 pas de catastrrphe sinon numéro de la catastrophes a definir pour chaque
    public Meteos(string nom,int[] saisonPourcent, int cata){
        Nom = nom;
        SaisonPourcent = saisonPourcent;
        NuméroCata = cata;
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
