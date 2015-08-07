/*global Backbone */
var app = app || {};

(function () {
    'use strict';
    
    app.QuestionModel = Backbone.Model.extend({
        urlRoot: '/api/Question',
        defaults: {
            segmentId: 0,
            text: '',
            answers: []
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