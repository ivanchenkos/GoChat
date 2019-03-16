$(document).ready(function (){
	var kol = $('div.input').text();
	
		if (kol.length>93) {
			$('div.fon_input').css('height','auto');
		}
		else{
			$('div.fon_input').css('height',8+"%");
		}
		console.log(kol.length);
});
var socket = new WebSocket("ws://localhost:8181/reg");
function send(obj) {
    var SendMessage = {
        Commandtype: "msg",
        SendMessage: document.getElementById("messageM").textContent
    }
    var sndmsg = JSON.stringify(SendMessage);
	socket.send(sndmsg);
	
    document.getElementById("messageM").textContent = ""
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
        if (response_parse.Resp == 'sucs') {
            document.body.innerHTML = document.querySelector('#chat-template').innerHTML;
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
	//find massage
	var massage = document.getElementById('root')
	//create padding top
	var padding = document.createElement('div')
	padding.style = "padding-top: 100px;" 
	massage.appendChild(padding)
	//create head layer
	var head_lyaer = document.createElement('div');
	head_lyaer.className = "head_lyaer"
	massage.appendChild(head_lyaer)
	//create subord layer
	var subordinate_layer = document.createElement('div');
	subordinate_layer.className = "subordinate_layer"
	subordinate_layer.type = "text"
	head_lyaer.appendChild(subordinate_layer)
	//create <p>
	var ptext = document.createElement('p')
	ptext.className = "text"
	ptext.innerHTML = nick + " : " + message
	subordinate_layer.appendChild(ptext)
	
	console.log(message)
    //messageElem.innerHTML = "<p>" + nick + " : " + message + "</p>";
    //document.getElementById('root').appendChild(messageElem);
}
