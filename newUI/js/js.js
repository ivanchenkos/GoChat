
var socket = new WebSocket("ws://localhost:8181/reg");
function send(obj) {
    var SendMessage = {
        Commandtype: "msg",
        SendMessage: $('#messageM').val()
    }
    var sndmsg = JSON.stringify(SendMessage);
	socket.send(sndmsg);
    
    $('#messageM').val( function(index, oldhtml) {
        return ""
    } );
    
}
function pressKey(e, obj) {
    if (e.keyCode == 13 && !e.shiftKey) {
        
        send(obj);
        
    } else if (e.keyCode == 13 && e.shiftKey ) {
        
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
            alert("Аккаунт создан");
        } else {
            alert("Ошибка создания аккаунта")
        }
    }
    if (response_parse.Commandtype == 'log') {
        console.log(response_parse.Resp)
        console.log(response_parse.Commandtype)
        if (response_parse.Resp == 'sucs') {
            document.body.innerHTML = document.querySelector('#chat-template').innerHTML;
            var screen_height = screen.height,
			screen_chat,
			screen_input,
				screen_input=screen_height*0.75;
				screen_chat=screen_height*0.70;
				$('div.fon_input').css('top',screen_input);
				$('div.massage').css('height',screen_chat);
			console.log(screen_height);
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
	//Автоматический скролл вниз
	var chatResult = $('div.massage');
    chatResult.scrollTop(chatResult.prop('scrollHeight'));
	console.log(message);
    //messageElem.innerHTML = "<p>" + nick + " : " + message + "</p>";
    //document.getElementById('root').appendChild(messageElem);
}
