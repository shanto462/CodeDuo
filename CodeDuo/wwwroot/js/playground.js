"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/playgroundHub").build();
var isConnected = false;

connection.start().then(function () {
    document.getElementById("btnSend").disabled = false;

    var codeId = document.getElementById("codeId").value;
    var userId = document.getElementById("userId").value;

    connection.invoke("RegisterForCode", codeId, userId).then((result) => {
        if (result === false)
            throw "Registration failed";
        isConnected = result;
    }).catch(function (err) {
        return console.error(err.toString());
    });

}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("btnSend").disabled = true;

connection.on("ReceiveBroadCast", async function (user, message) {
    if (user != document.getElementById("userId").value)
        document.getElementById("codeSegment").value = (message);
});

var textArea = document.getElementById("codeSegment");

textArea.addEventListener("input", async function () {
    var value = textArea.value;
    value = (value)
    var user = document.getElementById("userId").value;
    var codeId = document.getElementById("codeId").value;
    connection.invoke("UpdateCode", user, codeId, value, 0).catch(function (err) {
        return console.error(err.toString());
    });
});