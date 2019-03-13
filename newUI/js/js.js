
$(document).ready(function (){
	var text_length=$('div.subordinate_layer').text().length,
		height_text = $('#text');
	$('div.head_lyaer').css('width',height_text.css('width'));	
	$('div.head_lyaer').css('height',height_text.css('height'));
	console.log(height_text.css('height'));
	console.log($('div.head_lyaer').css('height'));

});
