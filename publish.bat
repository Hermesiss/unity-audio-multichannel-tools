copy README.md "Packages/trismegistus.unity.audio-toolkit/ReadMe.md" /Y
copy CHANGELOG.md "Packages/trismegistus.unity.audio-toolkit/CHANGELOG.md" /Y
cd Packages/trismegistus.unity.audio-toolkit
npm publish --registry http://upm.trismegistus.tech:4873/ || pause