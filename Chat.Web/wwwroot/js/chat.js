"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatter").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("receive", function (message) {
    var encodedMsg = message.userName + " says " + message.text;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var message = document.getElementById("messageInput").value;
    var chatMessage = {
        text: message,
        userId: loggedUserId,
        userName: loggedUserName
    };
    debugger;
    connection.invoke("SendAll", chatMessage).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});