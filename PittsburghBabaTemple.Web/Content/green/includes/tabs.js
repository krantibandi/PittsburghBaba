jQuery.preloadImages = function()
{
  for(var i = 0; i<arguments.length; i++)
  {
    jQuery("<img>").attr("src", arguments[i]);
  }
}

$(document).ready(function(){
	$.fadespeed = 180; // Number of milliseconds for fade transition to last.
	var $tabs = $('#tabs').tabs({ fx: { opacity: 'toggle', duration: $.fadespeed }, selected: 0 }); // Set selected to whatever you want the default tab to be; zero-indexed

	
	// If you don't want a fade transition, comment out the above and use the following:
	// $.fadespeed = 0;
	// $('#tabs').tabs({ selected: 0 }); // Set selected to whatever you want the default tab to be; zero-indexed

	$.auto = true; // Whether or not the tabs will automatically switch.
	$.tabtime = 12000; // If you select auto, how much time in milliseconds each tab will be displayed for

	if ($.auto) {
		overviewClick(); // Base this on whatever tab is selected first e.g. if selected is 0, call overviewClick()
	}
	
	$.overviewtab = 'images/overview_tab.png';
	$.overviewtab_active = 'images/overview_tabactive.png';
	$.ourworktab = 'images/ourwork_tab.png';
	$.ourworktab_active = 'images/ourwork_tabactive.png';
	$.servicestab = 'images/services_tab.png';
	$.servicestab_active = 'images/services_tabactive.png';
	$.contacttab = 'images/contact_tab.png';
	$.contacttab_active = 'images/contact_tabactive.png';
	$.whytab = 'images/why_tab.png';
	$.whytab_active = 'images/why_tabactive.png';
	
	$.preloadImages($.overviewtab, $.overviewtab_active, $.ourworktab, $.ourworktab_active, $.servicestab, $.servicestab_active, $.contacttab, $.contacttab_active, $.whytab, $.whytab_active);

	var timeout;

	function overviewClick() {
		setTimeout("$('#ourworktab').attr('src', $.ourworktab);", $.fadespeed);
		setTimeout("$('#servicestab').attr('src', $.servicestab);", $.fadespeed);
		setTimeout("$('#contacttab').attr('src', $.contacttab);", $.fadespeed);
		setTimeout("$('#whytab').attr('src', $.whytab);", $.fadespeed);
	    	setTimeout("$('#overviewtab').attr('src', $.overviewtab_active);", $.fadespeed);
		if ($.auto) {
			clearTimeout(timeout);
			timeout = setTimeout("$('#ourworktab').click();", $.tabtime);
		}
	}
	
	function ourworkClick() {
		setTimeout("$('#overviewtab').attr('src', $.overviewtab);", $.fadespeed);
		setTimeout("$('#servicestab').attr('src', $.servicestab);", $.fadespeed);
		setTimeout("$('#contacttab').attr('src', $.contacttab);", $.fadespeed);
		setTimeout("$('#whytab').attr('src', $.whytab);", $.fadespeed);
		setTimeout("$('#ourworktab').attr('src', $.ourworktab_active);", $.fadespeed);
		if ($.auto) {
			clearTimeout(timeout);
			timeout = setTimeout("$('#servicestab').click();", $.tabtime);
		}
	}
	
	function servicesClick() {
		setTimeout("$('#overviewtab').attr('src', $.overviewtab);", $.fadespeed);
		setTimeout("$('#ourworktab').attr('src', $.ourworktab);", $.fadespeed);
		setTimeout("$('#contacttab').attr('src', $.contacttab);", $.fadespeed);
		setTimeout("$('#whytab').attr('src', $.whytab);", $.fadespeed);
		setTimeout("$('#servicestab').attr('src', $.servicestab_active);", $.fadespeed);
		if ($.auto) {
			clearTimeout(timeout);
			timeout = setTimeout("$('#contacttab').click();", $.tabtime);
		}
	}
	
	function contactClick() {
		setTimeout("$('#overviewtab').attr('src', $.overviewtab);", $.fadespeed);
		setTimeout("$('#ourworktab').attr('src', $.ourworktab);", $.fadespeed);
		setTimeout("$('#servicestab').attr('src', $.servicestab);", $.fadespeed);
		setTimeout("$('#whytab').attr('src', $.whytab);", $.fadespeed);
		setTimeout("$('#contacttab').attr('src', $.contacttab_active);", $.fadespeed);
		if ($.auto) {
			clearTimeout(timeout);
			timeout = setTimeout("$('#whytab').click();", $.tabtime);
		}
	}
	
	function whyClick() {
		setTimeout("$('#overviewtab').attr('src', $.overviewtab);", $.fadespeed);
		setTimeout("$('#ourworktab').attr('src', $.ourworktab);", $.fadespeed);
		setTimeout("$('#servicestab').attr('src', $.servicestab);", $.fadespeed);
		setTimeout("$('#contacttab').attr('src', $.contacttab);", $.fadespeed);
		setTimeout("$('#whytab').attr('src', $.whytab_active);", $.fadespeed);
		if ($.auto) {
			clearTimeout(timeout);
			timeout = setTimeout("$('#overviewtab').click();", $.tabtime);
		}
	}
	
	$('#overviewtab').click(function() {
		overviewClick();
 	});
	
	$('#ourworktab').click(function() {
		ourworkClick();
 	});
	
	$('#servicestab').click(function() {
		servicesClick();
 	});
	
	$('#contacttab').click(function() {
		contactClick();
 	});
	
	$('#whytab').click(function() {
		whyClick();
 	});
	
	
});