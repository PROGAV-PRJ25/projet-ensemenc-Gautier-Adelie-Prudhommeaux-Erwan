public class Jardin{
    public int TourActuel {get;set;}
    public int Poudredetoile {get;set;}
    public List<Saisons> ListeSaison {get;set;}
    public Saisons Saison {get;set;}
    public Meteos Meteo {get;set;}
    public int[,] MatPlante {get;set;} 
    public int[,] MatAnimaux {get;set;} 
    public string[] ActionPossible {get;set;}
    public int NombreAction {get;set;}
    public int[] Objects {get;set;}// chaque indice correspond a un object. il y en a 25 
    public string[] PlantesJouable {get;set;}
    public string[] MaladiesPossible {get;set;}
    public string[] ObjectsAchetable {get;set;} 
    public int[] GrainesDisponibles {get;set;} // chaque indice est assosier a une plante. il y en a 11 
    public List<Plantes> ListPlante {get;set;}
    public List<Animaux> ListAnimaux {get;set;}

    

    public Jardin(){
        TourActuel = 0;
        NombreAction = 3;
        Poudredetoile = 100;
        Meteo = new Calme();
        ListeSaison = new List<Saisons> {new Saion1(),new Saion2(),new Saion3()}
        Saison = ListeSaison[0];
        PlantesJouable = ["Etoile", "Météorite", "Rose", "Chapeau", "Nuage"];
        ActionPossible = ["Objet","Planter","Récolter","Déraciné","Arroser","Protéger","Effrayer","Applaudir"];
        MaladiesPossible = ["Maladie1"];
        ObjectsAchetable = ["Lanterne","Pelle","Écharpe","Paravent","Clôture","Arrosoir","Épouventails","Haut parleur","Médicament","Pommade"];
        Objects = [0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0];
        GrainesDisponibles = [10,0,0,0,0,0,0,0,0,0,0];
        MatPlante = new int[21,21];
        MatAnimaux = new int[21,21];
        ListAnimaux = new List<Animaux> {};
        ListPlante = new List<Plantes> {};
        for (int i = 7; i<14;i++){
            for (int j = 7; j<14;j++){
                MatPlante[i,j]= -1;
                MatAnimaux[i,j]= -1;
            }
        }

    }

    public override string ToString()
    {
        return Calme();
    }
}





public class Maladies {
    public string Nom {get; set;}
    public int[] Dégats {get; set;}
    public int TauxApparition {get; set;}
    public int Medicament {get; set;}

    public Maladies(string nom, int[] dégats, int tauxApparition) {
        Nom = nom;
        Dégats = dégats; //nb de dégâts par tour
        TauxApparition = tauxApparition;
        Medicament = 0;
    }
}

public class Mildiou : Maladies {
    public Mildiou() : base("Mildiou", [-8, 0, 0, 0], 2) {}
}

public class Rouille : Maladies {
    public Rouille() : base("Rouille", [-1, 0, 0, 0], 2) {}
}

public class GrandeSoif : Maladies {
    public GrandeSoif() : base("Grande soif", [0, -4, 0, 0], 4) {}
}

public class Isolement : Maladies {
    public Isolement() : base("Isolement", [-5, 0, 0, 0], 15) {}
}

public class PeurNoir : Maladies {
    public PeurNoir() : base("Peur du noir", [-5, 0, 0, 0], 4) {}
}

public class Infertilite : Maladies {
    public Infertilite() : base("Infertilité", [0, 0, 0, 0], 2) {}
}

public class Explosion : Maladies {
    public Explosion() : base("Explosion", [-1, 0, 0, 0], 15) {}
}