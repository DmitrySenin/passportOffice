;(function() {
	'use strict';
	
	angular
		.module('main')
		.controller('SearchCtrl', SearchCtrl);

	PersonalInfoCtrl.$inject = ['PersonalInfoLoader'];

	function SearchCtrl(PersonalInfoLoader) {
		var vm = this;

		vm.SearchingOptions = new PersonalInfoLoader.SearchingOptions();
	}
})();