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
    description: "Fast paced and exciting game for 4 players. You'll be surprised at how simple, yet exhausting a ball game can be.",
    credits: "Mads Falkenberg Sønderstrup,Kasper Christian Hansen,Per Josefsen and Rasmus Nagel",
    gamelink: "prototypes/prototype1/WallBall.zip"
}

var prototype2 = {
    name: "Prototype 2",
    description: "TBD",
    credits: "TBD",
    gamelink: ""
}

var prototype3 = {
    name: "Prototype 3",
    description: "TBD",
    credits: "TBD",
    gamelink: ""
}

var prototype4 = {
    name: "Prototype 4",
    description: "TBD",
    credits: "TBD",
    gamelink: ""
}

var prototype5 = {
    name: "Prototype 5",
    description: "TBD",
    credits: "TBD",
    gamelink: ""
}

var prototype6 = {
    name: "Prototype 6",
    description: "TBD",
    credits: "TBD",
    gamelink: ""
}

var prototype7 = {
    name: "Prototype 7",
    description: "TBD",
    credits: "TBD",
    gamelink: ""
}

var prototype8 = {
    name: "Prototype 8",
    description: "TBD",
    credits: "TBD",
    gamelink: ""
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