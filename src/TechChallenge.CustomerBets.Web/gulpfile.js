﻿/// <binding Clean='clean' />

var gulp = require("gulp"),
    rimraf = require("rimraf"),
    concat = require("gulp-concat"),
    cssmin = require("gulp-cssmin"),
    uglify = require("gulp-uglify");


var paths = {
    webroot: "./",
    bower: "./bower_components/",
    lib: "./ClientThirdParty/"
};

paths.js = paths.webroot + "js/**/*.js";
paths.minJs = paths.webroot + "js/**/*.min.js";
paths.css = paths.webroot + "css/**/*.css";
paths.minCss = paths.webroot + "css/**/*.min.css";
paths.concatJsDest = paths.webroot + "js/site.min.js";
paths.concatCssDest = paths.webroot + "css/site.min.css";

gulp.task("clean:js", function (cb) {
    rimraf(paths.concatJsDest, cb);
});

gulp.task("clean:css", function (cb) {
    rimraf(paths.concatCssDest, cb);
});

gulp.task("clean", ["clean:js", "clean:css"]);

gulp.task("copy", ["clean"], function () {
    var bower = {
        "bootstrap": paths.bower + "bootstrap/dist/**/*.{js,map,css,ttf,svg,woff,woff2,eot,otf}",
        "jquery": paths.bower + "jquery/dist/jquery*.{js,map}",
        "font-awesome": paths.bower + "font-awesome/**/*.{js,map,css,ttf,svg,woff,woff2,eot,otf}",
        "datatables": paths.bower + "datatables/media/**/*.{js,map,css,png,psd}"
    }

    for (var destinationDir in bower) {
        gulp.src(bower[destinationDir])
            .pipe(gulp.dest(paths.lib + destinationDir));
    }
});


gulp.task("min:js", function () {
    gulp.src([paths.js, "!" + paths.minJs], { base: "." })
        .pipe(concat(paths.concatJsDest))
        .pipe(uglify())
        .pipe(gulp.dest("."));
});

gulp.task("min:css", function () {
    gulp.src([paths.css, "!" + paths.minCss])
        .pipe(concat(paths.concatCssDest))
        .pipe(cssmin())
        .pipe(gulp.dest("."));
});

gulp.task("min", ["min:js", "min:css"]);
