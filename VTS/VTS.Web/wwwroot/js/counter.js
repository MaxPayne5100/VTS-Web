//js code for counting animation
$(document).ready(function () 
{
	//variable for сreating animation only once
	IsAnimated = false 

	$(window).on('scroll' , function(){
		scroll_pos = $(window).scrollTop() + $(window).height();
		element_pos = $('.vac-info').offset().top + $('.vac-info').height()/10;
		
		// if statement for сreating animation only in specified position and once
		if (scroll_pos > element_pos && !IsAnimated) { 
			$('.count').each(function() {
				$(this).prop('Counter',0).animate({
					Counter:$(this).text()
				}, {
					duration: 1700,
					easing: 'swing',
					step: function () {
						$(this).text(Math.ceil(this.Counter));
					}
				});
			});

			IsAnimated = true
		};
	});
});