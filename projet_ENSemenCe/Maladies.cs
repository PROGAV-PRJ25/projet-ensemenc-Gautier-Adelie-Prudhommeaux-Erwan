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