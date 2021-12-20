notification = {
    info: function (message, delay) {
        notification.show(message, "alert alert-info", delay);
    },
    success: function (message, delay) {
        notification.show(message, "alert alert-success", delay);
    },
    alert: function (message, delay) {
        notification.show(message, "alert alert-warning", delay);
    },
    error: function (message, delay) {
        notification.show(message, "alert alert-danger", delay);
    },
    show: function (message, type, delay) {
        var target = notification.get();
        var bar = $("<div class='notibar " + type + "  alert-dismissable' style='display: none;'><button type='button' class='close' data-dismiss='alert' aria-hidden='true'>&times;</button><a class='close'></a><p>" + message + "</p></div>");
        target.append(bar);
        bar.slideDown("fast");
        var time = parseInt(delay);
        if (!isNaN(time) && time > 0) {
            setInterval(function() {
                notification.remove(bar);
            }, time);
        }
        bar.find("a.close").click(function () {
            notification.remove(bar);
        });
    },
    remove: function (target) {
        target.slideUp("fast", function () {
            target.remove();
        });
    },
    clear: function () {
        var target = notification.get();
        target.find("div.notibar").each(function () {
            notification.remove($(this));
        });
    },
    get: function () {
        var content = $("#pageContent");
        var target = content.find("#notifyBox");
        if (target.length == 0) {
            target = $("<div id='notifyBox'></div>");
            content.prepend(target);
        }
        return target;
    }
};