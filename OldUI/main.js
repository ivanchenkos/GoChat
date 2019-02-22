var socket = new WebSocket("ws://localhost:8181/reg");
function send(obj) {
    var SendMessage = {
        Commandtype: "msg",
        SendMessage: document.getElementById("messageM").value
    }
    var sndmsg = JSON.stringify(SendMessage);
    socket.send(sndmsg);
    obj.value = ""
}
function pressKey(e, obj) {
    if (e.keyCode == 13) {
        
        send(obj);
        
        return false;
    }
}
function sendReg() {
    var REGmessage = {
        Commandtype: "reg",
        Nickname: document.getElementById("nickReg").value,
        Password: document.getElementById("PassReg").value
    }
    var REGmessageJson = JSON.stringify(REGmessage);
    socket.send(REGmessageJson);
}
socket.onmessage = function (event) {
    var response = event.data;
    var response_parse = JSON.parse(response)
    if (response_parse.Commandtype == 'msg') {
        showMessage(response_parse.SendMessage, response_parse.NickName);
    }
    if (response_parse.Commandtype == "reg") {
        if (response_parse.resp == "sucs") {
            alert("Аккаунт создан")
        } else {
            alert("Ошибка создания аккаунта")
        }
    }
    if (response_parse.Commandtype == 'log') {
        console.log(response_parse.Resp)
        console.log(response_parse.Commandtype)
        console.log(response_parse.NewPage)
        if (response_parse.Resp == 'sucs') {
            document.body.innerHTML = response_parse.NewPage;
        } else {
            alert("Ошибка входа")
        }
    }
}
function sendLog() {
    var REGmessage = {
        Commandtype: "log",
        Nickname: document.getElementById("nickLog").value,
        Password: document.getElementById("PassLog").value
    }
    var REGmessageJson = JSON.stringify(REGmessage)
    socket.send(REGmessageJson)
}

// показать сообщение в div#subscribe
function showMessage(message, nick) {
    var messageElem = document.createElement('div');
    messageElem.innerHTML = "<p>" + nick + " : " + message + "</p>";
    document.getElementById('root').appendChild(messageElem);
}
