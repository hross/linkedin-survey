/*global Backbone */
var app = app || {};

(function () {
    'use strict';

    app.SurveyModel = Backbone.Model.extend({
        urlRoot: '/api/Survey',
        defaults: {
            suveyId: 0,
            text: '',
        },
        initialize: function (data) {
            if (data.url) {
                this.urlRoot = data.url;
            }
        }
    });
})();