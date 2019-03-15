
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
//93
//length