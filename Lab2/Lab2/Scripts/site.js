$(document).ready(function () {
    $('td > input').change(function () {
        var pos = this.parentNode.attributes.getNamedItem('id').nodeValue.split('|');
        console.log(pos);
        if (pos[1] > 0)
            updateMatrix();
        if (pos[1] == 0 && pos[0] > 0) {
            var id = '0|' + pos[0];
            document.getElementById(id).innerHTML = this.value;
        }
        initChart();

    });
    updateMatrix();
    initChart();
});

function updateMatrix() {
    var rowCount = $('.calculated:last')[0].id.split('|')[0];
    rowCount++;
    $('.calculated').each(function (index) {
        var pos = this.id.split('|');
        this.setAttribute('accurate', (parseFloat(document.getElementById(rowCount + "|" + pos[1]).firstChild.nextSibling.value)
            / parseFloat(document.getElementById(rowCount + "|" + pos[0]).firstChild.nextSibling.value)));
        this.innerHTML = parseFloat(this.getAttribute('accurate')).toFixed(4);
    });

    $('.calculated-sum').each(function (index) {
        var pos = this.id.split('|')[1];
        var sum = 0;
            $('.calculated').each(function (index) {
                if (this.id.split('|')[1] == pos)
                    sum += parseFloat(this.getAttribute('accurate'));
            });
            this.innerHTML = sum.toFixed(4);
            this.setAttribute('accurate', sum);
    });

    var maxReversed = -10000.0;
    $('.calculated-sum-revers').each(function (index) {
        var pos = this.id.split('|');
        pos[0]--;
        var sum = 0;
        var sumNode = document.getElementById(pos[0] + '|' + pos[1]);
        var val = 1.0 / parseFloat(sumNode.getAttribute('accurate'));
        this.innerHTML = val.toFixed(4);
        if (val > maxReversed)
            maxReversed = val;
    });
    $('.calculated-func').each(function (index) {
        var pos = this.id.split('|');
        pos[0]-=2;
        var sum = 0;
        var sumNode = document.getElementById(pos[0] + '|' + pos[1]);
        this.innerHTML = (1.0 / parseFloat(sumNode.getAttribute('accurate')) / maxReversed).toFixed(4);
    });
}

//initChart("funcChart", "Функція належності до нечіткої множини",
//    [@(String.Join(", ",
//        Model.BaseParams)))],
//[@(String.Join(", ",
//    Model.QuestioningData.Rank["Початківець"].Questions.
//        Select(q => q.AnswerMark)))], 
//["rgba(0, 200, 83, 0.4)",
//    "rgba(0, 200, 83, 0.4)",
//    "rgba(0, 200, 83, 0.4)",
//    "rgba(0, 200, 83, 0.4)"]);