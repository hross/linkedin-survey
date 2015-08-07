/*global Backbone, jQuery, _, ENTER_KEY */
var app = app || {};

(function ($) {
    'use strict';

    app.SegmentView = Backbone.View.extend({
        // our template - compiled via underscore
        segmentTemplate: _.template($('#segment-template').html()),

        events: {
            "click .start.button": "start"
        },
        start: function (ev) {
            // move to the first question
            var id = $(ev.target).data('id');
            Backbone.history.navigate('#/question/' + id);
            return false;
        },
        initialize: function (options) {
            this.$el.show();

            if (!options.model) {
                this.model = options;
            }

            this.model.bind("change", this.render, this);   
        },
        render: function (eventName) {
            this.$el.html(this.segmentTemplate(this.model.toJSON()));
        },
        hide: function () {
            this.$el.hide();
        },
        close: function(){
            //this.remove();
            //this.unbind();
            if (this.model) {
                this.model.unbind("change", this.render);
            }
        }
    });

})(jQuery);