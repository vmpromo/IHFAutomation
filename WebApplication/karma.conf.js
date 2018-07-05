module.exports = function(config) {
    // Documentation: https://karma-runner.github.io/0.13/config/configuration-file.html
    config.set({
        browsers: [ 'ChromeHeadless' ],

        files: [
			'Scripts/underscore-min.js',
            'Scripts/angular-1.2.32.min.js',
			'Scripts/virtualConsole.js',
            'Scripts/returnsConstants.js',
            'Scripts/returnsServiceWrapper.js',
            'Scripts/AuthoriseDialog.js',
			'Scripts/returns.js',

            'node_modules/angular-mocks/angular-mocks.js',
            'JsTests/**/*.tests.js',
        ],

        port: 9876,

        frameworks: [ 'jasmine', 'phantomjs-shim' ],

        logLevel: config.LOG_WARN, //config.LOG_DEBUG

        // reporter options
        mochaReporter: {
            colors: {
                success: 'bgGreen',
                info: 'cyan',
                warning: 'bgBlue',
                error: 'bgRed'
            }
        },

        coverageReporter: {
            reporters:[
                {type: 'text'},
                {type: 'text-summary'}
            ],
        }
    });
};