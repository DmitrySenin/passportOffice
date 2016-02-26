;(function() {
	'use strict';

	angular
		.module('main')
		.controller("PersonalInfoCtrl", PersonalInfoCtrl);

	PersonalInfoCtrl.$inject = ['PersonalInfoService', 'EventNames', '$rootScope', 'usSpinnerService'];

	function PersonalInfoCtrl(PersonalInfoService, EventNames, $rootScope, usSpinnerService) {

		var vm = this,
			currentPage = 1,
			pageSize = 20,
			allDataLoaded = false,
			fullSort = false,
			searchingOptions = new PersonalInfoService.SearchingOptions();

		vm.PersonalInfo = [];
		vm.SpinnerKey = 'LoadingSpinner';
		vm.ShowAdminPanel = false;

		/**
		 * Load personal data and set value of context variables.
		 */
		vm.getInfo = function() {
			startSpinner();
			PersonalInfoService.Load(pageSize, currentPage, fullSort, searchingOptions)
				.success(function(data) {
					// Handle gathered data.
					data.map(function(item, index, arr) {
						arr[index].BirthdayDate = new Date(item.BirthdayDate);
						arr[index].PassportIssueDate = new Date(item.PassportIssueDate);
						vm.PersonalInfo.push(arr[index]);
					});

					// If loaded portion of data less than requested
					if(data.length < pageSize) {
						allDataLoaded = true;
					}
				})
				.error(function(error) {
					vm.error = error;
				})
				.finally(function() {
					stopSpinner();
					vm.InfiniteScroll.inLoading = false;
				});
		}

		// Load personal data.
		vm.getInfo();

		// Inifinite scroll.
		vm.InfiniteScroll = {
			inLoading: false,
			nextPage: function() {
				if(!allDataLoaded) {
					this.inLoading = true;
					currentPage++;
					vm.getInfo();
				}
			}
		};

		/**
		 * Send request to remove all personal data and clear local storage.
		 */
		vm.RemoveAllPersonalData = function() {
			PersonalInfoService.RemoveAll().success(function() {
				vm.PersonalInfo = [];
			}).error(function(error) {
				alert('Operation failed.');
			});
		}

		/**
		 * Handles of changing searching parameters.
		 * @param  {Object} event Description of handled event.
		 * @param  {Object} SearchingOptions Object with criteria of data selection.
		 * @param  {Boolean} FullSort Flag which identifies that data should be sorted by all discussed fields.
		 */
		$rootScope.$on(EventNames.Search, function(event, SearchingOptions, FullSort) {
			fullSort = FullSort;
			searchingOptions = SearchingOptions;
			currentPage = 1;
			allDataLoaded = false;
			vm.PersonalInfo = [];
			vm.getInfo();
		});

		/**
		 * Handles administrator authentication.
		 * @param  {Object} event Description of handled event.
		 */
		$rootScope.$on(EventNames.AuthAdmin, function(event) {
			vm.ShowAdminPanel = true;
		});

		/**
		 * Handles user logout.
		 * @param  {Object} event Description of handled event.
		 */
		$rootScope.$on(EventNames.Logout, function(event) {
			vm.ShowAdminPanel = false;
		});

		function startSpinner() {
			usSpinnerService.spin(vm.SpinnerKey);
		}

		function stopSpinner() {
			usSpinnerService.stop(vm.SpinnerKey);
		}
	}
})();