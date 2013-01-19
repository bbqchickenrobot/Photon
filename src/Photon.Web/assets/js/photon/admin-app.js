Photon.Admin = Photon.Admin || {};
Photon.Admin.App = {
	init: function(){
		//data
		this.albums = new Photon.AlbumCollection();
		
		//configure views
		this.breadcrumbsView = new Photon.Admin.BreadcrumbsView({
			el: "#breadcrumbs"
		});
		
		this.albumListView = new Photon.AlbumListView({
			collection: this.albums
		});

		//start the app
		this.start();
	},
	start: function(){
		this.albums.fetch();
	}
};