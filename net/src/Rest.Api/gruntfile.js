module.exports = function (grunt) {
    grunt.loadNpmTasks('grunt-contrib-uglify');
    grunt.loadNpmTasks('grunt-contrib-watch');
    grunt.loadNpmTasks('grunt-contrib-concat');
    grunt.loadNpmTasks('grunt-contrib-clean');
    grunt.loadNpmTasks('grunt-contrib-jshint');
    grunt.loadNpmTasks("grunt-jscs");
    grunt.loadNpmTasks('grunt-contrib-csslint');
    grunt.loadNpmTasks('grunt-contrib-cssmin');

    grunt.initConfig({
        uglify: {
            my_target: {
                files: { 'wwwroot/app.js': ['Scripts/app.js', 'Scripts/**/*.js'] }
            }
        },
        watch: {
            scripts: {
                files: ['Scripts/**/*.js'],
                tasks: ['uglify']
            }
        },
        cssmin: {
            dist: {
                files: {
                    'dist/css/style.min.css': ['src/css/style.css']
                }
            }
        },
        csslint: {
            lax: {
                options: {
                    'import': false
                },
                src: ['src/css/**/*.css']
            },
        },
        clean: {
            dist: ['dist']
        },
        jshint: {
            files: ['Gruntfile.js', 'src/js/custom.js'],
            options: {
                globals: {
                    jQuery: true
                }
            }
        },
        jscs: {
            src: "src/js/custom.js",
            options: {
                config: ".jscsrc",
                requireCurlyBraces: ["if"]
            }
        },
    });

    grunt.registerTask('default', ['uglify', 'watch']);
};