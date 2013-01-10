/* JS for Portfolio Navigation System */

$(document).ready(function(){
	var total = 3; // Total number of slides: must change if you want to add or remove portfolio items
	var current = 0; // Current slide: can be changed 
	var slideHeight = 400 + 50; // Single slide height (50 top-margin)
	var slides = $('.slide'); // Selector for slides
	var slidesHeight = slides.length * slideHeight; // Total slide Height
	
	$('#pn').css('overflow', 'hidden');
	$('#pn').css('width', '1000px');
	$('#next').click(function() { // Next button clicked
		current -= 1;
		if (current <= (0 - slidesHeight / 450)) current = 0;
		$('.slide').animate({
			top: current*slideHeight + "px"
		});
    });
	$('#prev').click(function() { // Prev button clicked
		current += 1;
		if (current > 0) current = 0 - (slidesHeight / 450 - 1);
		$('.slide').animate({
			top: current*slideHeight + "px"
		});
    });
});