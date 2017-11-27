function GetModal(id) {
    var prototypeData = {
        name: "",
        description: "",
        credits: "",
        gamelink: ""
    }
    switch (id) {
        case 1:
            prototypeData = prototype1;
            break;
        case 2:
            prototypeData = prototype2;
            break;
        case 3:
            prototypeData = prototype3;
            break;
        case 4:
            prototypeData = prototype4;
            break;
        case 5:
            prototypeData = prototype5;
            break;
        case 6:
            prototypeData = prototype6;
            break;
        case 7:
            prototypeData = prototype7;
            break;
        case 8:
            prototypeData = prototype8;
            break;
        case 9:
            prototypeData = prototype9;
            break;
        case 10:
            prototypeData = prototype10;
            break;
        default:
            prototypeData = prototypeBlank;
            break;
    }

    $('#game-info-modal').modal('show');
    $('#game-info-modal-title').text(prototypeData.name);
    $('#game-info-modal-body').text(prototypeData.description);
    $('#game-info-modal-credits').text("Credits: " + prototypeData.credits);
    $('#game-info-modal-download').attr("href", prototypeData.gamelink);
}

var prototypeBlank = {
    name: "Prototype",
    description: "TBD",
    credits: "TBD",
    gamelink: "#"
}

var prototype1 = {
    name: "WallBall",
    description: "Prototype 1 - Fast paced and exciting game for 4 players. You'll be surprised at how simple, yet exhausting a ball game can be.",
    credits: "Mads Falkenberg Sønderstrup, Kasper Christian Hansen, Per Josefsen, Rasmus Nagel, and Marie-Louise Alexius Sørensen",
    gamelink: "prototypes/prototype1/WallBall.zip"
}

var prototype2 = {
    name: "SPACE BARBARIANS",
    description: "Prototype 2 - Spectate-able 2-player figthing game. Engage in fast-paced combat in 01:59 minutes.",
    credits: "Victor Olsson, Jonas Christensen, Lorena Ciobanu, Rares Popa, and Marie-Louise Alexius Sørensen",
    gamelink: "prototypes/prototype2/SpaceBarbarians.zip"
}

var prototype3 = {
    name: "PACMAN AFTER DARK",
    description: "Prototype 3 - Modification of existing game. Spin on the arcade classic, that implements stealth in a dark maze.",
    credits: "Mads Engberg, Martin Sørensen, Sarah Grossi, Tamas Lakatos, and Marie-Louise Alexius Sørensen",
    gamelink: "prototypes/prototype3/PacManAfterDark.zip"
}

var prototype4 = {
    name: "ANIMAL SAVING",
    description: "Prototype 4 - Emotion to Game. Save the animals of this idyllic farm from the grizzly horrors of the spreading animal-disease.",
    credits: "Mads Falkenberg Sønderstrup, Lorena Ciobanu, Astrid Knappmann, Mathies Wiencke, and Marie-Louise Alexius Sørensen",
    gamelink: "prototypes/prototype4/AnimalSaving.zip"
}

var prototype5 = {
    name: "SOLARSCAPE",
    description: "Play as the small alien Dash, who must save themself from the death of their galaxy, by building a rocket-ship and flying off into space.",
    credits: "Lorena Ciobanu, Luisa Zurlo, Nicola Zaltron, Rares Popa, Victor Olsson, and Marie-Louise Alexius Sørensen",
    gamelink: "prototypes/prototype5/Solarscape.zip"
}

var prototype6 = {
    name: "GRAVITY MATTERS",
    description: "A fast-paced, one-button game, in which you control an alien on a mad dash through a foreign environment, trying to evade obstacles and pulling all the levers.",
    credits: "Lorena Ciobanu, Mads Falkenberg Sønderstrup, Astrid Knappmann, Danyang Wang, and Marie-Louise Alexius Sørensen",
    gamelink: "prototypes/prototype6/Gravitymatters.zip"
}

var prototype7 = {
    name: "BALLOON",
    description: "A Vertical Jumping game, in which the player gently pushes their red balloon upwards, in a fever dream-like nightsky of piano music and rocekt ships.",
    credits: "Rasmus Nagel & Marie-Louise Alexius Sørensen",
    gamelink: "prototypes/prototype7/Balloons.zip"
}

var prototype8 = {
    name: "Zinder",
    description: "A digital local multiplayer, in which 4 players via controllers control a scientist determined to woo a zombie with present, and no be the last - and thusly eaten.",
    credits: "Lorena Ciobanu, Mads Falkenberg Sønderstrup, Astrid Knappmann, Ida Broni Christensen, and Marie-Louise Alexius Sørensen",
    gamelink: "prototypes/prototype8/Zinder.zip"
}

var prototype9 = {
    name: "Prototype 9",
    description: "TBD",
    credits: "TBD",
    gamelink: ""
}

var prototype10 = {
    name: "Prototype 10",
    description: "TBD",
    credits: "TBD",
    gamelink: ""
}