Photon.Album = Backbone.Model.extend({
	urlRoot: "/albums",
	idAttribute: "Id"
});

Photon.AlbumCollection = Backbone.Collection.extend({
	url: "/albums",
	model: Photon.Album
});