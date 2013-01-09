window.AlbumListView = Backbone.View.extend({

    tagName:'ul',

    className:'nav nav-list',

    initialize:function () {
        var self = this;
        this.model.bind("reset", this.render, this);
        this.model.bind("add", function (album) {
            $(self.el).append(new AlbumListItemView({model:album}).render().el);
        });
    },

    render:function () {
        $(this.el).empty();
        _.each(this.model.models, function (album) {
            $(this.el).append(new AlbumListItemView({model:album}).render().el);
        }, this);
        return this;
    }
});

window.AlbumListItemView = Backbone.View.extend({

    tagName:"li",

    initialize:function () {
        this.model.bind("change", this.render, this);
        this.model.bind("destroy", this.close, this);
    },

    render:function () {
        $(this.el).html(this.template(this.model.toJSON()));
        return this;
    }

});