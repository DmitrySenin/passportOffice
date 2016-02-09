;(function() {
	'use strict';

	angular
		.module('main')
		.controller("PersonalInfoCtrl", PersonalInfoCtrl);

	PersonalInfoCtrl.$inject = ['PersonalInfoLoader'];

	function PersonalInfoCtrl(PersonalInfoLoader) {

		var vm = this,
			currentPage = 1,
			pageSize = 20;

		vm.SearchingOptions = new PersonalInfoLoader.SearchingOptions();

		/**
		 * Load personal data and set value of context variables.
		 */
		vm.getInfo = function() {
			PersonalInfoLoader.Load(pageSize, currentPage, vm.SearchingOptions)
				.success(function(data) {
					vm.PersonalInfo = data;
					vm.PersonalInfo.map(function(item, index, arr) {
						arr[index].BirthdayDate = new Date(item.BirthdayDate);
						arr[index].PassportIssueDate = new Date(item.PassportIssueDate);
					});
				})
				.error(function(error) {
					vm.error = error;
				});
		}

		// Load personal data.
		vm.getInfo();

		// Represents datepicker's state.
		vm.dateprickerPopup = {
			opened: false
		};

		// Open datepicker.
		vm.openDatepickerPopup = function() {
			vm.dateprickerPopup.opened = true;
		};
	}
})();