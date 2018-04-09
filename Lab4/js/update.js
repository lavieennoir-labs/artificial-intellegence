function getInputCellInfo (input) {
	var parentTableId = input.parentElement.parentElement.parentElement.parentElement.id;
	var row = 0;
	var col = 0;

	for (var i = 0; i < input.classList.length; i++) {
		if(input.classList[i].startsWith('table-row-'))
			row = input.classList[i].replace('table-row-', '');
		else if (input.classList[i].startsWith('table-col-'))
			col = input.classList[i].replace('table-col-', '');
	}

	var info = {
		node : input,
		tableId : parentTableId,
		i : parseInt(row),
		j : parseInt(col)
	} 
	return info;
}

function getCell (tableId, i, j) {	
	return $('#' + tableId + ' > tbody > tr > td > .table-col-' + j + ',' +
	 	'#' + tableId + ' > tbody > tr > th > .table-col-' + j + ',' +
	 	'#' + tableId + ' > tbody > tr > .table-col-' + j + '')[i];
}

function ReadInputData () {
	A = [
		[0,0],
		[0,0],
		[0,0],
		[0,0],
		[0,0],
		[0,0],
	];
	B = [
		[0,0],
		[0,0],
		[0,0],
		[0,0],
		[0,0],
		[0,0],
	];
	AQuote = [
		[0,0],
		[0,0],
		[0,0],
		[0,0],
		[0,0],
		[0,0],
	];
	BQuote = [
		[0,0],
		[0,0],
		[0,0],
		[0,0],
		[0,0],
		[0,0],
	];
	FazificationArr = [
		[0,0,0,0,0,0],
		[0,0,0,0,0,0],
		[0,0,0,0,0,0],
		[0,0,0,0,0,0],
		[0,0,0,0,0,0],
		[0,0,0,0,0,0],
	];
	FuzzyCounclusionArr = [
		[0,0,0,0,0,0],
		[0,0,0,0,0,0],
		[0,0,0,0,0,0],
		[0,0,0,0,0,0],
		[0,0,0,0,0,0],
		[0,0,0,0,0,0],
	];

	for(var i = 0; i < FuzzySetLength; i++) {		
		BQuote[i][0] = Math.round(parseFloat(getCell('fazification-table', 0, 2 + i).value) * 10) / 10;
		
		for(var j = 0; j < 2; j++){
			A[i][j] = Math.round(parseFloat(getCell('fazification-table', 2 + i, j).value) * 10) / 10;
			B[i][j] = Math.round(parseFloat(getCell('fazification-table', j, 2 + i).value) * 10) / 10;
			AQuote[i][j] = Math.round(parseFloat(getCell('inputАQuote', j, i + 1).value) * 10) / 10;
		}
	}
	relationType = parseInt($("input[name='relation-radio']:checked")[0].id.replace('relation-type-', ''));
}

function FuzzyOutput () {
	CountFazification();
	CountFuzzyConclusion();
	CountDefadzification();
	UpdateViews();
}

function CountFazification() {
	if(relationType == 0)
		CountMinImplication();
	else if(relationType == 1)
		CountExtenderImplication();
	else if(relationType == 2)
		CountLucashevichImplication();
}

function CountMinImplication() {
	for(var i = 0; i < FuzzySetLength; i++)
		for(var j = 0; j < FuzzySetLength; j++)
			FazificationArr[i][j] = Math.round(Math.min(A[i][1], B[j][1]) * 10) / 10;
}

function CountExtenderImplication() {
	for(var i = 0; i < FuzzySetLength; i++)
		for(var j = 0; j < FuzzySetLength; j++)
			FazificationArr[i][j] = Math.round(Math.max(B[j][1],1 - A[i][1]) * 10) / 10;
}

function CountLucashevichImplication() {
	for(var i = 0; i < FuzzySetLength; i++)
		for(var j = 0; j < FuzzySetLength; j++)
			FazificationArr[i][j] = Math.round(Math.min(1, 1 - A[i][1] + B[j][1]) * 10) / 10;
}


function CountFuzzyConclusion() {
	for(var j = 0; j < FuzzySetLength; j++) {
		var col = [];
		for(var i = 0; i < FuzzySetLength; i++) {
			FuzzyCounclusionArr[i][j] = Math.round(Math.min(FazificationArr[i][j], AQuote[i][1]) * 10) / 10;
			col.push(FuzzyCounclusionArr[i][j]);
		}
		BQuote[j][1] = Math.max.apply(Math, col);
	}

}

function CountDefadzification() {
	V = 0;
	var sum = 0;
	for(var i = 0; i < FuzzySetLength; i++) {
		V += BQuote[i][0] * BQuote[i][1];
		sum += BQuote[i][1]
	}
	V = Math.round(V / sum);
}

function UpdateViews() {
	var currentCell = null;
	for(var i = 0; i < FuzzySetLength; i++) {
		for(var j = 0; j < 2; j++){
			currentCell = getCell('fuzzy-table', j, i + 2);
			currentCell.innerHTML = B[i][j];

			currentCell = getCell('fuzzy-table', i + 2, j);
			currentCell.innerHTML = AQuote[i][j];

			currentCell = getCell('fuzzy-table', j + 8, i + 2);
			currentCell.innerHTML = BQuote[i][j];
		}
		for(var j = 0; j < FuzzySetLength; j++){
			currentCell = getCell('fazification-table', i + 2, j + 2);
			currentCell.innerHTML = FazificationArr[i][j];
			currentCell = getCell('fuzzy-table', i + 2, j + 2);
			currentCell.innerHTML = FuzzyCounclusionArr[i][j];
		}
	}
	$('.defadzification-value').each(function(index) {
		$(this)[0].innerHTML = V;
	});
}

var relationType = 0;

var FuzzySetLength = 6;
var A = [];
var B = [];

var FazificationArr = [];
var FuzzyCounclusionArr = [];

var AQuote = []; //input array
var BQuote = []; //output array
var V = 0; //output value;


$(document).ready(function(){
	ReadInputData();
	FuzzyOutput();

	$('input').on('input', function() { 
		var input  = $(this)[0];
    	var inputInfo = getInputCellInfo(input);

    	if(inputInfo.tableId == 'inputАQuote')
    		AQuote[inputInfo.j - 1][inputInfo.i] = Math.round(parseFloat(input.value) * 10) / 10;
    	else if(inputInfo.tableId == 'fazification-table' && inputInfo.i < 2) {
    		B[inputInfo.j - 2][inputInfo.i] = Math.round(parseFloat(input.value) * 10) / 10;
    		if(i == 0)
    			BQuote[inputInfo.j - 2][inputInfo.i] = Math.round(parseFloat(input.value) * 10) / 10;
    	}
    	else if(inputInfo.tableId == 'fazification-table' && inputInfo.i < 2)
    		A[inputInfo.i - 2][inputInfo.j] = Math.round(parseFloat(input.value) * 10) / 10;

    	FuzzyOutput();
	});


	$("input[name='relation-radio']").click(function() {
		relationType = parseInt(this.id.replace('relation-type-', ''));

    	FuzzyOutput();
	});

});
