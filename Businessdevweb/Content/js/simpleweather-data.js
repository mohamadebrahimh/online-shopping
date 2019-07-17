/*SimpleWeather Init*/

"use strict";

$(document).ready(function() {  
  getWeather(); //Get the initial weather.
  getCurrentTime(); //Get the initial weather.
  setInterval(getWeather, 10000); //Update time after every sec.
});
function PersianDay(day) {

    switch (day) {
        case "Sunday":
            return "یک شنبه";
        case "Monday":
            return "دوشنبه";
        case "Tuesday":
            return "سه شنبه";
        case "Wednesday":
            return "چهارشنبه";
        case "Thursday":
            return "پنج شنبه";
        case "Friday":
            return "جمعه";
        case "Saturday":
            return "شنبه";
        case "Sun":
            return "یک شنبه";
        case "Mon":
            return "دوشنبه";
        case "Tue":
            return "سه شنبه";
        case "Wed":
            return "چهارشنبه";
        case "Thu":
            return "پنج شنبه";
        case "Fri":
            return "جمعه";
        case "Sat":
            return "شنبه";
        default:

    }
}
/*Current Time Cal*/
var getCurrentTime = function(){
	var nowDate = moment().format('L');
    var nowDay = moment().format('dddd');
    $('.nowday').html(PersianDay(nowDay));
	$('.nowdate').html(nowDate);
};

/*Get Current Weather*/
var getWeather = function() {
	
	if( $('#weather_2').length > 0 ){ 
		/*With Forcast*/
		$.simpleWeather({
            location: 'astara,gilan,IR',
		woeid: '',
		unit: 'c',
		success: function(weather) {
			var $this = $('#weather_2');
            var html="";
			
			html += '<ul class="forcast-days">';
			
			/*Add below snippet if forcast required*/
            for (var i = 0; i < weather.forecast.length - 3; i++) {

                html += '<li><span class="forcast-day block">' + PersianDay(weather.forecast[i].day) + '</span><img class="block" src="../../Content/img/weathericons/' + weather.forecast[i].code + '.svg"/><span class="forcast-high-deg block">' + weather.forecast[i].high + '&deg;C</span></li>';
			}
			html += '</ul>';
			$this.find(".weather").html(html);
		},
		error: function(error) {
			$this.find(".weather").html('<p>'+error+'</p>');
		}
	  });
	}
 
    if( $('#weather_1').length > 0 ){
    
		/*Without Forcast*/
		$.simpleWeather({
            location: 'astara,gilan,IR',
		woeid: '',
		unit: 'c',
		success: function(weather) {
			var $this = $('#weather_1');
			var html='<span class="block temprature"><span class="unit"> &deg;'+weather.units.temp+'</span>'+weather.temp+'</span>';
			$this.find('.left-block').html(html);
			//alert(this.id);
			html='<span class="block temprature-icon"><img src="../../Content/img/weathericons/'+weather.code+'.svg"/></span><h6>'+weather.city+'</h6>';
			
			$this.find('.right-block').html(html);
		},
		error: function(error) {
			console.log(error);
		}
	  });
   }
   
   
};	

