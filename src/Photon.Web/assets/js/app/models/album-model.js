window.Album = Backbone.Model.extend({

    urlRoot:"/album",

    initialize:function () {
        
    }

});

window.AlbumCollection = Backbone.Collection.extend({

    model: Album,

    url:"/album/",

    findByName:function (key) {
        var url = '/album/search/' + key;
        var self = this;
        $.ajax({
            url:url,
            dataType:"json",
            success:function (data) {
                self.reset(data);
            }
        });
    }
});