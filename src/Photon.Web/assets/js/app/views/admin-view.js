(function ($) {
	AdminView = Backbone.View.extend({
		el: $("#admin-view-container"),
		initialize: function () {
			this.albums = new AlbumCollection( null, { view: this });
		},
		render: function(){
			
		}
	});
	
	var adminView = new AdminView;
})(jQuery);