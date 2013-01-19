Photon.Admin.BreadcrumbsView = Backbone.View.extend({
	render: function(){
		this.$el.html("<li><a href='#albums'>Albums</a></li>");
		return this;
	},
	events: {
		"click a": "sayHello"
	},
	sayHello: function(){
		alert("Hello");
	}
});