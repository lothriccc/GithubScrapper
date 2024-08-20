const accordions = document.querySelector('.section__container_body')
const accordion  = accordions.querySelectorAll('.content');

accordion.forEach(item => {
    item.querySelector('.content__body').classList.add('accordion')
    item.addEventListener('click' , e => {
        item.querySelector('.content__body').classList.toggle('accordion')
    })
})
// Sample data


// Populate repository details
document.getElementById('commit-count').innerText = repoDetails.commits;
document.getElementById('branch-count').innerText = repoDetails.branches;
document.getElementById('star-count').innerText = repoDetails.stars;
document.getElementById('watching-count').innerText = repoDetails.watchers;
document.getElementById('fork-count').innerText = repoDetails.forks;

// Populate file list
const fileList = document.getElementById('file-list');
repoDetails.files.forEach(file => {
    const li = document.createElement('li');
    li.innerText = file;
    fileList.appendChild(li);
});

document.getElementById('total-commit-count').innerText = repoDetails.commits;

// Create the language usage chart
const ctx = document.getElementById('languageChart').getContext('2d');
const languageChart = new Chart(ctx, {
    type: 'doughnut',
    data: {
        labels: Object.keys(repoDetails.languageUsage),
        datasets: [{
            label: 'Language Usage',
            data: Object.values(repoDetails.languageUsage),
            backgroundColor: ['#F1E05A', '#E34C26', '#563D7C'],
        }]
    },
    options: {
        responsive: true,
        plugins: {
            legend: {
                position: 'top',
            }
        }
    }
});