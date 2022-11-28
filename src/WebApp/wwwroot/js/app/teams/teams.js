(function ($) {
    $.fn.extend({
        initTeamsFunctionality: function (options) {
            const { servicesApiUrl } = options;

            $.get(`${servicesApiUrl}/teams`, function(data) {
                $("#teams-list").empty();
                $.each(data, (i, t)=>{
                    $("#teams-list").append(`<a href="#" data-teamid="${t.id}" class="list-group-item list-group-item-action">${t.city.name} ${t.name}</a>`);
                });
            });

            $("#teams-list").on("click", "a", function() {
                $("#players").show();
                
                const teamId = $(this).data('teamid');
                $.get(`${servicesApiUrl}/teams/${teamId}/players`, function(data) {
                    $("#players-list").empty();
                    $.each(data, (i, p)=>{
                        $("#players-list").append(`<div class="list-group-item">${p.first_name} ${p.last_name}</div>`);
                    });
                    if (!data.length) {
                        $("#players-list").text("No players data for such team.");
                    }
                });

                return false;
            });

            return this;
        }
    });
})(jQuery);
