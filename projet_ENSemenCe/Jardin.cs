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
    public int[] GrainesDisponibles {get;set;} // chaque indice est associé a une plante. il y en a 11 
    public List<Plantes> ListPlante {get;set;}
    public List<Animaux> ListAnimaux {get;set;}
    

    

    public Jardin(){
        TourActuel = 0;
        NombreAction = 3;
        Poudredetoile = 100;
        Meteo = new Calme();
        ListeSaison = new List<Saisons> {new Saison1(),new Saison2(),new Saison3()};
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
        for (int i = 0; i<7;i++){
            for (int j = 0; j<7;j++){
                MatPlante[i,j]= -1;
                MatAnimaux[i,j]= -1;
            }
        }
    }

    public Plantes RechercherPlante(int[] co) {
        int indiceCherché = MatPlante[co[0], co[1]];
        foreach(Plantes plante in ListPlante) {
            if (plante.Id == indiceCherché) {return plante;}
        }
        return new Plantes("",[],"",false,new Saison1(),"",0,[],[],0,0,new List<Maladies> {},0,0, "", new Jardin());  
    }

    public Animaux RechercherAnimaux(int[] co) {
        int indiceCherché = MatAnimaux[co[0], co[1]];
        foreach(Animaux animal in ListAnimaux) {
            if (animal.Id == indiceCherché) {return animal;}
        }
        return new Animaux("",[],0,0,new List<int> {},new Jardin(), "");
    }

    public void SupprimerPlante(int[] co) {
        Plantes planteSup = RechercherPlante(co);
        if (planteSup.Nom != "") {
            ListPlante.Remove(planteSup);
            for (int i=0; i<21; i++) {
                for (int j=0; j<21; j++) {
                    if (MatPlante[i,j] == planteSup.Id) {
                        MatPlante[i,j] = -1;
                    }
                }
            }
        }
    }

    public void SupprimerAnimaux(int[] co) {
        Animaux animalSup = RechercherAnimaux(co);
        if (animalSup.Nom != "") {
            ListAnimaux.Remove(animalSup);
            for (int i=0; i<21; i++) {
                for (int j=0; j<21; j++) {
                    if (MatAnimaux[i,j] == animalSup.Id) {
                        MatAnimaux[i,j] = -1;
                    }
                }
            }
        }
    }

    public int[] DeplacementDirigéAnimaux(int[] coAnimal, int[] coCible)
    {
        int diffX = coCible[0] - coAnimal[0];
        int diffY = coCible[1] - coAnimal[1];
        int[] deplacement = [0, 0];
        if (diffX != 0 && diffY != 0) { deplacement = [diffX / Math.Abs(diffX), diffY / Math.Abs(diffY)]; }
        else if (diffY != 0) { deplacement = [0, diffY / Math.Abs(diffY)]; }
        else if (diffX != 0) { deplacement = [diffX / Math.Abs(diffX), 0]; }

        if (MatAnimaux[coAnimal[0] + deplacement[0], coAnimal[1] + deplacement[1]] != -1) {
            if (diffY / Math.Abs(diffY) <= 0) {
                if (MatAnimaux[coAnimal[0] + deplacement[0], coAnimal[1] + deplacement[1] + 1] != -1) { deplacement = [0, 0]; }
                else { deplacement[1] = deplacement[1] + 1; }
            }
            else {
                if (MatAnimaux[coAnimal[0] + deplacement[0], coAnimal[1] + deplacement[1] - 1] != -1) { deplacement = [0, 0]; }
                else { deplacement[1] = deplacement[1] - 1; }
            }
        }
        return deplacement;
    }

    public int[] RechercherPlanteProche(int[] coOrigine, string planteCherchée) { //planteCherchée correspond au nom de la plante la plus proche recherchée. Si on veut regarder toutes les plantes, mettre ""
        int[] coPlanteProche = [coOrigine[0], coOrigine[1]];
        double distPlanteProche = 1000;
        for (int i = 0; i < 21; i++) {
            for (int j = 0; j < 21; j++) {
                if (MatPlante[i, j] > 0 && (planteCherchée=="" || RechercherPlante([i,j]).Nom==planteCherchée)) {
                    double dist = Math.Sqrt(Math.Pow(coOrigine[0] - i, 2) + Math.Pow(coOrigine[1] - j, 2));
                    if (dist < distPlanteProche) {
                        distPlanteProche = dist;
                        coPlanteProche = [i, j];
                    }
                }
            }
        }
        return coPlanteProche;
    }

    public int[] RechercherAnimalProche(int[] coOrigine, string animalCherché) { //planteCherchée correspond au nom de la plante la plus proche recherchée. Si on veut regarder toutes les plantes, mettre ""
        int[] coAnimalProche = [coOrigine[0], coOrigine[1]];
        double distAnimalProche = 1000;
        for (int i = 0; i < 21; i++) {
            for (int j = 0; j < 21; j++) {
                if (MatAnimaux[i, j] > 0 && (animalCherché=="" || RechercherAnimaux([i,j]).Nom==animalCherché)) {
                    double dist = Math.Sqrt(Math.Pow(coOrigine[0] - i, 2) + Math.Pow(coOrigine[1] - j, 2));
                    if (dist < distAnimalProche) {
                        distAnimalProche = dist;
                        coAnimalProche = [i, j];
                    }
                }
            }
        }
        return coAnimalProche;
    }

    public override string ToString()
    {
        string resultat = "||=========================================||=========================================||=========================================||\n";
        for (int i = 0; i < 3; i++)
        {
            for (int m = 0; m < 7; m++)
            {

                resultat += "||-----------------------------------------||-----------------------------------------||-----------------------------------------||\n";
                resultat += "||";
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 7; k++)
                    {

                        string données = Convert.ToString(MatPlante[i * 7 + m, j * 7 + k]);
                        if (MatPlante[i * 7 + m, j * 7 + k] == -1)
                        {
                            resultat += "     |";
                        }
                        else if (MatPlante[i * 7 + m, j * 7 + k] == 0)
                        {
                            resultat += "  >< |";
                        }
                        else
                        {
                            resultat += " ";
                            resultat += RechercherAnimaux([i * 7 + m, j * 7 + k]).Emoji;
                            resultat += " ";
                            resultat += RechercherPlante([i * 7 + m, j * 7 + k]).Emoji;
                            resultat += " |";
                        }
                    }
                    resultat += "|";
                }
                resultat += "\n";
            }
            resultat += "||=========================================||=========================================||=========================================||\n";
        }
        return resultat;
    }
}

