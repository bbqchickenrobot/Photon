
window.Router = Backbone.Router.extend({

	routes: {
		"": "home",
		"admin": "adminHome",
		"album/:id": "albumDetails"
	},

	initialize: function () {
		this.headerView = new AlbumsView();
		$('.header').html(this.headerView.render().el);

		// Close the search dropdown on click anywhere in the UI
		$('body').click(function () {
			$('.dropdown').removeClass("open");
		});
	},

});

templateLoader.load(["AlbumsList", "AlbumDetails"], function () {
	app = new Router();
	Backbone.history.start();
});