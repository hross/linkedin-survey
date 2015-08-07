/*global Backbone */
var app = app || {};

(function () {
    'use strict';
    
    var AppRouter = Backbone.Router.extend({
        surveyId: 1,
        routes: {
            "" : "main",
            "survey/:id": "surveyDetails",
            "segment/:id": "segmentDetails",
            "question/:id": "questionDetails",
            "next/segment/:id": "nextSegment",
            "next/question/:id": "nextQuestion",
            "finish" : "finish"
        },
        main: function () {
            this.clearViews();

            Backbone.history.navigate('#/survey/1');
        },
        surveyDetails: function(id) {
            this.clearViews();

            this.surveyId = id;
            this.survey = new app.SurveyModel({ id: id });

            if (this.surveyView) {
                this.surveyView.initialize({ model: this.survey });
            } else {
                this.surveyView = new app.SurveyView({ model: this.survey, el: $("#survey_container") });
            }
            this.survey.fetch({});
        },
        segmentDetails: function (id) {
            this.clearViews();

            this.segment = new app.SegmentModel({ id: id });

            if (this.segmentView) {
                this.segmentView.initialize(this.segment);
            } else {
                this.segmentView = new app.SegmentView({ model: this.segment, el: $("#segment_container") });
            }
            this.segment.fetch({});
        },
        questionDetails: function (id) {
            this.clearViews();

            this.question = new app.QuestionModel({ id: id });

            if (this.questionView) {
                this.questionView.initialize(this.question);
                this.questionView.render();
            } else {
                this.questionView = new app.QuestionView({ model: this.question, el: $("#question_container") });
            }
            this.questionView.render();
            this.question.fetch({});
        },
        finish: function () {
            this.clearViews();

            $('#finish').show();
        },
        nextSegment: function (segmentId) {
            var segments = new app.SegmentCollection();
            segments.fetch({
                data: { surveyId: this.surveyId },
                success: function () {
                    var next = segments.nextSegment(segmentId);
                    // find the next segment id
                    if (next > 0) {
                        return Backbone.history.navigate('#/segment/' + next);
                    } else {
                        // given the current segment id, find the next one
                        return Backbone.history.navigate('#/finish');
                    }
                }
            });
        },
        clearViews: function () {
            $('#finish').hide();

            if (this.questionView) {
                this.questionView.hide();
                //this.questionView.close();
            }
            if (this.segmentView) {
                this.segmentView.hide();
                //this.segmentView.close();
            }
            if (this.surveyView) {
                this.surveyView.hide();
                //this.surveyView.close();
            }
        }
    });

    app.AppRouter = new AppRouter();
    Backbone.history.start();
})();