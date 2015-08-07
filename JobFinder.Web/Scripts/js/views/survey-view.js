/*global Backbone, jQuery, _, ENTER_KEY */
var app = app || {};

(function ($) {
    'use strict';

    app.SurveyView = Backbone.View.extend({
        // our template - compiled via underscore
        surveyTemplate: _.template($('#survey-template').html()),

        events: {
            "click .start.button": "start"
        },
        start: function (ev) {
            // move to the first segment
            var id = $(ev.target).data('id');
            Backbone.history.navigate('#/segment/' + id);
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
            this.$el.html(this.surveyTemplate(this.model.toJSON()));
        },
        hide: function () {
            this.$el.hide();
        },
        close: function(){
            this.remove();
            this.unbind();
            if (this.model) {
                this.model.unbind("change", this.render);
            }
        }
    });

})(jQuery);