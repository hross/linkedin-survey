/*global Backbone */
var app = app || {};

(function () {
    'use strict';

    app.SegmentModel = Backbone.Model.extend({
        urlRoot: '/api/Segment',
        defaults: {
            surveyId: 0,
            text: '',
            description: ''
        },
        initialize: function (data) {
            if (data.url) {
                this.urlRoot = data.url;
            }
        }
    });

    app.SegmentCollection = Backbone.Collection.extend({
        model: app.SegmentModel,
        url: "../api/Segment",
        nextSegment: function (segmentId) {
            if (!segmentId) {
                return this.first().get('id');
            } else {
                var res = -1;
                this.each(function (model) {
                    if (segmentId == model.get('id')) {
                        res = model.get('nextId');
                    }
                });
                return res;

            }
        }
    });
})();