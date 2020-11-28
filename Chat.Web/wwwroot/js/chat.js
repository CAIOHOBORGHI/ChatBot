"use strict";

/* js functions */
function sendMessage() {
    var message = document.getElementById("messageInput").value;
    var chatMessage = {
        text: message,
        userId: loggedUserId,
        userName: loggedUserName
    };
    connection.invoke("SendAll", chatMessage).catch(function (err) {
        return console.error(err.toString());
    });
}

/* Starting SignalR connection */
var connection = new signalR.HubConnectionBuilder().withUrl("/chatter").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("receive", function (message) {
    var encodedMsg = message.userName + " says " + message.text;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    li.className = message.userId === loggedUserId ? "me" : "him";
    document.getElementById("messagesList").appendChild(li);
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.querySelector("#messageInput").addEventListener('keypress', function (e) {
    if (e.key == 'Enter') {
        sendMessage();
    }
    e.preventDefault();
})

document.getElementById("sendButton").addEventListener("click", function (event) {
    sendMessage();
    event.preventDefault();
});