public class Jardin{
    public int TourActuel {get;set;}
    public int Argent {get;set;}
    public Saisons Saison {get;set;}
    public Meteos Meteo {get;set;}
    public MatrixNode Modelisation {get;set;} // définir a chaque numéro une plante ou un animal ou objet 
    public string[] ActionPossible {get;set;}
    public int NombreAction {get;set;}
    public int[] Objects {get;set;}// chaque indice correspond a un object. il y en a 23 

    public string[] PlantesJouable {get;set;}
    public string[] MaladiesPossible {get;set;}
    public string[] ObjectsAchetable {get;set;} 
     
    public int[] GrainesDisponibles {get;set;} // chaque indice est assosier a une plante. il y en a 11 
    
    


    public Jardin(){
        TourActuel = 0;
        Argent = 100;
        PlantesJouable = ["Etoile", "Météorite", "Rose", "Chapeau", "Nuage"];
        MaladiesPossible = ["Maladie1"];
        ObjectsAchetable = ["Lanterne","Pelle","Écharpe","Paravent","Clôture","Arrosoir","Épouventails","Haut parleur"];
        Objects = [0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0];
        GrainesDisponibles = [10,0,0,0,0,0,0,0,0,0,0];
        int[,] mat = new int[7,7];
        Modelisation = new MatrixNode(mat , 1);
    }
}


public class MatrixNode
{
    public int[,] Matrix { get; set; }
    public int planete { get; set; }
    public MatrixNode Nord { get; set; } 
    public MatrixNode Sud { get; set; }
    public MatrixNode Est  { get; set; }
    public MatrixNode Ouest  { get; set; }

    public MatrixNode(int[,] matrix, int numplanete)
    {
        Matrix = matrix;
        planete = numplanete;
    }


    public void AddMatrix(MatrixNode origin, int[,] newMatrix, string direction, int numplanete) {
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
}