var FollowingService = function () {

    var createFollowing = function (FolloweeId, done, fail) {
        $.post("/api/followings", { FolloweeId: followeeId })
        .done(done)
        .fail(fail);
    };

    var deleteFollowing = function (FolloweeId, done, fail) {
        $.ajax({
            url: "/api/followings/" + FolloweeId,
            method: "DELETE"
        }).done(done)
            .fail(fail);
    };

    return {
        createFollowing: createFollowing,
        deleteFollowing: deleteFollowing
    }

}();