;angular.module('main').controller("PersonalInfoCtrl", ['$scope', 'PersonalInfoLoader',
	function($scope, PersonalInfoLoader) {

	getInfo();

	function getInfo() {
		PersonalInfoLoader.Load()
			.success(function(data) {
				$scope.PersonalInfo = data;
				$scope.PersonalInfo.map(function(item, index, arr) {
					arr[index].BirthdayDate = new Date(item.BirthdayDate);
					arr[index].PassportIssueDate = new Date(item.PassportIssueDate);
				});
			})
			.error(function(error) {
				$scope.error = error;
			});
	}

	// Represents datepicker's state.
	$scope.dateprickerPopup = {
		opened: false
	};

	// Open datepicker.
	$scope.openDatepickerPopup = function() {
		$scope.dateprickerPopup.opened = true;
	};
}]);