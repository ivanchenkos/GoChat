$(document).ready(function (){
		var colors = ['#00a5bd','#347f8a','#4973c7'];
		var first = 0;
		var counter = 0;
		var balls = document.querySelectorAll('.ball');
		function colorize(){
			counter = first;
			first++;
			if (first>colors.lenght-1) {first=0};
			balls.forEach(function(ball,n){
				ball.className='ball';
				ball.classList.add('ball_'+colors[counter]);
				counter++;
				if (counter>colors.lenght-1) {counter=0};
			});
			colorize();
			setInterval(colorize,4000);
		}
});