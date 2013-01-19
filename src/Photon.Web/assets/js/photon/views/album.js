Photon.NewAlbumView = Backbone.View.extend({
	events: {
		"submit form": "addAlbum"
	},
	addAlbum: function(e){
		e.preventDefault();
		var albumName = $("#albumName").val(); 

		var newAlbum = new Photon.Album({
			Name: albumName
		});

		newAlbum.save();
		Photon.Admin.App.albums.add(newAlbum);
	},
	render: function(){
		var template = $("#new-album-template").html(); 
		var compiled = _.template(template, {});
		this.$el.html(compiled);
		return this;
	}
});

Photon.AlbumView = Backbone.View.extend({
	tagName: "li",
	className: "span3",
	events: {
		"click a.delete-album": "deleteAlbum",
		"click a.edit-album": "editAlbum",
	},
	deleteAlbum: function(e) {
		e.preventDefault();
		if(confirm("Are you sure?")){
			this.model.destroy();
			//console.log(Photon.Admin.App.albums.toJSON());
			//Photon.Admin.App.albums.remove(this.model);
			//console.log(Photon.Admin.App.albums.toJSON());
		}
		
	},
	editAlbum: function(e) {
		e.preventDefault();
		console.log("Not implemented yet...");
	},
	render: function(){
		var template = $("#album-template").html(); 
		var compiled = _.template(template, this.model.toJSON());
		this.$el.html(compiled);
		return this;
	}
});

Photon.AlbumListView = Backbone.View.extend({
	initialize: function(){
		this.collection.bind("reset", this.render, this);
		this.collection.bind("add", this.render, this);
		this.collection.bind("remove", this.render, this);
		this.renderNewAlbumView();
		//console.log("In AlbumList Init");
	},
	tagName: "ul",
	className: "thumbnails",
	renderNewAlbumView: function(){
		var newAlbumView = new Photon.NewAlbumView({
			el: "#new-album-container"
		});
		newAlbumView.render();
	},
	render: function(){
		//console.log("In AlbumList Render");
		var els = [];
		this.collection.each(function(item){
			var itemView = new Photon.AlbumView({
				model: item
			});
			els.push(itemView.render().el);
		});
		
		this.$el.html(els);
		$("#album-list").append(this.el);
		//return this;
	}
});