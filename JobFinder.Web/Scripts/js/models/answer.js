/*global Backbone */
var app = app || {};

(function () {
    'use strict';
    
    app.AnswerModel = Backbone.Model.extend({
        urlRoot: '/api/QuestionAnswer',
        defaults: {
            answerId: 0,
            questionId: 0,
        },
        initialize: function (data) {
            if (data.url) {
                this.urlRoot = data.url;
            }
        },
        reset: function () {
        }
    });
})();