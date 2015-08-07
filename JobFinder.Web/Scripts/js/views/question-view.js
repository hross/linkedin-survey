/*global Backbone, jQuery, _, ENTER_KEY */
var app = app || {};

(function ($) {
    'use strict';

    app.QuestionView = Backbone.View.extend({
        // our template - compiled via underscore
        questionTemplate: _.template($('#question-template').html()),

        events: {
            "click .answer.button": "answerClick"
        },
        answerClick: function (ev) {
            var id = $(ev.target).data('id');
            var nextQuestion = this.model.get('hasNextQuestion');

            //TODO: here is where we post a questionanswer model
            var answer = new app.AnswerModel({ questionId: this.model.get('id'), answerId: id });
            answer.save({});

            if (nextQuestion) {
                // go to the next question if there is one
                var nextId = this.model.get('nextId');
                Backbone.history.navigate('#/question/' + nextId);
            } else {
                // go to the next segment
                var segmentId = this.model.get('segmentId');
                Backbone.history.navigate('#/next/segment/' + segmentId);
            }

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
            this.$el.html(this.questionTemplate(this.model.toJSON()));
        },
        hide: function () {
            this.$el.hide();
        },
        close: function() {
            this.remove();
            this.unbind();
            if (this.model) {
                this.model.unbind("change", this.render);
            }
        }
    });

})(jQuery);